BlackJack
=========

Blackjack game implemented in C# using raw websockets.  Uses Reactjs on the frontend for views and frontend logic.

To run the project, install [node](http://node.org), then run the following commands:

1. ```npm install```
2. ```npm install -g webpack```
3. ```webpack```

This will build and bundle the front end JavaScript code.  Then just hit debug in Visual Studio.  For development, run ```webpack -w``` in the Blackjack.Web directory to keep from having to rebuild the front-end after each change.

To see it running, it's deployed [here](https://blackjack.azurewebsites.net) as an azure website.
