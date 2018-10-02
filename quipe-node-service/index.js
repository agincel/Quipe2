const express = require("express");
var app = express();

app.set("port", (process.env.PORT || 5000));
app.use(express.static(__dirname + "/public"));

const cors = require("cors"); //allow cross origin requests
app.use(cors());

const fs = require("fs");
const stream = require("stream");
const url = require("url");

const XLSX = require("xlsx");

var queuedID = "";
var folderName = "/users/";

function getUser(filePath) {
	var f = fs.readFileSync(filePath, 'utf8');
	if (f && f.length > 0) {
		f = JSON.parse(f);
		return f;
	} else {
		return null;
	}
}

function writeUser(o, filePath) {
	fs.writeFileSync(filePath, JSON.stringify(o));
}

function createUser(id, filePath) {
	var o = {};
	o.id = id;
	o.score = 0;
	fs.writeFileSync(filePath, JSON.stringify(o));
	return o;
}

function dateCheck(date) {
	return !date || Math.abs(new Date().getTime() - new Date(date)) > (1000 * 60 * 60 * 12); //if it has been at least 12 hours
}

//requires /foldername/dates.info which contains an array as follows:
//[ ["1/1/2017", "1/2/2017", "1/3/2017"], ["1/4/2017", "1/5/2017", "1/7/2017"] ] where the first array is dates of viable normals, and the second is dates of viable specials
// the request is made with /active?normal=x&special=y where normal is how many of the viable normal meetings are neeted and y is how many of the viable specials 
app.route("/active").get(function(req, res, next) {
	var queryObject = url.parse(req.url, true).query;
	if (queryObject.folder && queryObject.normal && queryObject.special) {
		var filePath = __dirname + "/" + queryObject.folder + "/dates.info";
		if (fs.existsSync(filePath)) {
			var f = fs.readFileSync(filePath, 'utf8');
			if (f && f.length > 0) {
				f = JSON.parse(f);
				var users = getAllUsers("/" + queryObject.folder + "/");
				var activeUsers = [];
				for (var i = 0; i < users.length; i++) { //for each user
					var normals = 0;
					var specials = 0;
					for (var j = 0; j < users[i].log.length; j++) { //for each logged date

						var ogDate = new Date(users[i].log[j]);
						var simplifiedDate = new Date((ogDate.getMonth() + 1).toString() + "/" + ogDate.getDate().toString() + "/" + ogDate.getFullYear().toString());

						for (var k = 0; k < f[0].length; k++) { //for each normal date
							if (simplifiedDate.getTime() == new Date(f[0][k]).getTime()) {
								normals += 1;
							}
						}

						for (var k = 0; k < f[1].length; k++) { //for each normal date
							if (simplifiedDate.getTime() == new Date(f[1][k]).getTime()) {
								specials += 1;
							}
						}
					}

					if (normals >= queryObject.normal && specials >= queryObject.special) {
						activeUsers.push(users[i]);
					}
				}

				var dispUsers = [];
				for (var i = 0; i < activeUsers.length; i++) {
					if (activeUsers[i].name) {
						dispUsers.push(activeUsers[i].name);
					} else {
						dispUsers.push(activeUsers[i].id);
					}
				}
				res.json(dispUsers);
			} else {
				res.json("error reading dates.info");
			}
		} else {
			res.json("Could not find /" + queryObject.folder + "/dates.info");
		}
	} else {
		res.json("Please invoke with /active?folder=foldername&normal=x&special=y");
	}
});

app.route("/queue").get(function(req, res, next) {
	var queryObject = url.parse(req.url, true).query;
	if (queryObject.id) {
		console.log(queryObject);
		console.log("Queuing queryObject.id: " + queryObject.id);
		queuedID = queryObject.id.toString();
		res.json("success");
	} else {
		if (queuedID != "") {
			res.json(queuedID);
			console.log("Dequeued " + queuedID);
			queuedID = "";
		} else {
			res.json("0");
		}
	}
});

app.route("/folder").get(function(req, res, next) {
	var queryObject = url.parse(req.url, true).query;
	if (queryObject.name) {
		console.log("Folder name: " + queryObject.name);
		folderName = "/" + queryObject.name + "/";
		res.json("success");

		if (!fs.existsSync(__dirname + folderName)) {
			fs.mkdirSync(__dirname + folderName);
		}
	}
});

app.route("/number").get(function(req, res, next) {
	var users = getAllUsers();
	var ret = 0;
	for (var i = 0; i < users.length; i++) {
		if (!dateCheck(users[i].date))
			ret += 1;
	}

	res.json(ret);
});

app.route("/names").get(function(req, res, next) {
	if (fs.existsSync(__dirname + folderName + 'names.xlsx')) {
		var workbook = XLSX.readFile(__dirname + folderName + 'names.xlsx');
		var sheet_name_list = workbook.SheetNames;
		var convJson = XLSX.utils.sheet_to_json(workbook.Sheets[sheet_name_list[0]]);
		console.log(convJson);
		for (var i = 0; i < convJson.length; i++) {
			var ID = convJson[i]["Username"].substring(0, 8);
			
			var filePath = __dirname + folderName + ID.toString() + ".json";
			if (fs.existsSync(filePath)) {
				var name = convJson[i]["First Name"] + " " + convJson[i]["Last Name"];
				console.log(ID + " " + name);
				var user = getUser(filePath);
				user.name = name;
				writeUser(user, filePath);
			}
		}
		res.json(getAllUsers());
	} else {
		res.json(folderName + "names.xlsx not found");
	}
});

app.route("/name").get(function(req, res, next) {
	var queryObject = url.parse(req.url, true).query;
	console.log(queryObject);
	if (queryObject.id && queryObject.name) {
		var filePath = __dirname + folderName + queryObject.id.toString() + ".json";
		if (fs.existsSync(filePath)) {
			var user = getUser(filePath);
			user.name = queryObject.name;
			writeUser(user, filePath);
			res.json("success");
		} else {
			res.json("id not found");
		}
	} else {
		res.json("please run with /name?user=id&name=name");
	}
});

app.route("/user").get(function(req, res, next) {
	var queryObject = url.parse(req.url, true).query;
	console.log(queryObject);

	if (queryObject.id && queryObject.add) { //adding points
		var filePath = __dirname + folderName + queryObject.id.toString() + ".json";
		var user = getUser(filePath);
		if (dateCheck(user.date)) { //if no date has been set, or it has been more than 12 hours
			if (parseInt(queryObject.add) > 15) {
				queryObject.add *= -1;  //subtract whatever they tried to add, then lock them out for 12 hours like normal
			}
			user.score = parseInt(user.score) + parseInt(queryObject.add);
			user.date = new Date();

			var hr = user.date.getHours();
			var logDate = new Date(user.date);
			if (hr <= 9) {
				logDate.setDate(user.date.getDate() - 1); //if before 9:59AM
				//set to yesterday's date for the purposes of logging (LANs go late)
			}
			if (user.log) {
				user.log.push(logDate);
			} else {
				user.log = [logDate];
			}

			writeUser(user, filePath);
			var disp = user.id.toString();
			if (user.name)
				disp = user.name;
			res.json(disp + "|" + user.score.toString() + "|");
		} else {
			var disp = user.id.toString();
			if (user.name)
				disp = user.name;
			console.log(user.score.toString())
			res.json(disp + "|-" + user.score.toString() + "|");
		}
	}
	else if (queryObject.id) { //getting user info
		var filePath = __dirname + folderName + queryObject.id.toString() + ".json";
		if (fs.existsSync(filePath)) { //get user, return ID or Name (if exists), plus score -- send id_or_name|score|
			var data = getUser(filePath);
			var disp = data.id;
			if (data.name) 
				disp = data.name;
			
			if (dateCheck(data.date))
				res.json(disp + "|" + data.score + "|");
			else 
				res.json(disp + "|-" + data.score + "|");
		} else { //create new user, send id|score|
			var o = createUser(queryObject.id, filePath);
			res.json(o.id + "|" + o.score + "|");
		}		
	} else { //bad request
		res.json(" |0|");
	}
});

function getAllUsers(folder) {
	var allUsers = [];
	var fN = folderName;
	if (folder) {
		fN = folder;
	}
	fs.readdirSync(__dirname + fN).forEach(file => {
		if (file.endsWith(".json")) {
			var f = fs.readFileSync(__dirname + fN + file, 'utf8');
			console.log(f);
			if (f.length > 0) {
				f = JSON.parse(f);
				allUsers.push(f);
			}
		}
	});

	allUsers.sort(function(a, b){
	    // Compare the 2 scores
	    if(a.score < b.score) return 1;
	    if(a.score > b.score) return -1; //reverse sorting
	    return 0;
	});
	return allUsers
}

app.route("/leaders").get(function(req, res, next) {
	var users = getAllUsers();
	for (var i = 0; i < users.length; i++) {
		users[i].hasSwiped = !dateCheck(users[i].date);
	}
	res.json(users);
});


var server = app.listen(app.get("port"), function() {
	console.log("Listening on port " + app.get("port"));
});