# Quipe 2
Quipe is a program designed for use with DuckLink at Stevens Institute of Technology circa 2017-2018. The program has three parts, that work in a rather unique tech stack: The WiNG Stack.

In practice, this allows the user to swipe their ID card one time; this one swipe will both log them as having attended an event, and will also serve as the beginning of their "account" playing a game; as of right now, Pachinko.

## The WinForms Application
Opening this Winforms application is the first step. It automatically opens a browser to a specified link (the web-hosted version of the game, which we'll get to), and brings up a window with its own embedded web view. The embedded web view is linked to the DuckLink event swipe-in page. You begin by entering your event's swipe code into that page, which then enables card swiping.

You must then run the node process with `node index.js` in the correct folder. It will run a process that listens on localhost:5000. That process is responsible for storing users, their scores, the dates of the events they attended, etc. Upon Receiving a swipe, the Winforms application receives it first. (To make sure of this, you must manually click your mouse into the swipe text field of the Winforms window, not in the embedded web view). It then performs two actions at once: first, it sends that same swipe to the embedded web view, logging a swipe in the DuckLink system. After that, it sends a modified version of that info through an HTTP request to localhost:5000, which queues up the swiped user ID in the system.

The game will later dequeue that same ID. Finally, the Winforms application can request a leaderboard from the node process with a button in the top right. Only press that button once service is live. Once you do, the service will return a JSON array of the scoreboard in descending order, and the Winforms application will display it.

## The Node Application
The Node service uses Express to listen to port 5000, waiting for certain requests in order to update, send, or receive information. Its core functionality involves storing the score for each user, and supporting "enqueueing" and "dequeueing" players in real time.

The WinForms service enqueues a swiped ID with the Node service. The game continually polls the Node service, and once it sees an enqueued ID, it dequeues it and plays a game using the account ID it received. Once the game is done, the game sends some resulting information back to the Node service, with the player ID and their new updated score. The Node service then updates and saves this info for next time.

The Node service also is able to send Leaderboard info. More advanced web views also support displaying a full list of Active Members based on parameters.

## The GameMaker: Studio Application
The GameMaker: Studio application (the game) continually polls localhost:5000 for a new ID to play a game with. Once it receives one, it performs an action with that account. In the current case, it drops a pachinko ball into a board, which can land in many different slots worth different score amounts. Once a score is received, the game sends a request back to the Node server, including that player's new score plus their ID. This information is stored back on the Node side.