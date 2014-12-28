﻿var Blackjack = (function () {
  var self = this,
    websocket;


  var blackjack = function () {
    self.game = ko.observable();
    self.connectionStatus = ko.observable("Not Connected");
    self.playerName = ko.observable();
  };

  self.connect = function () {
    var uri = 'ws://localhost:51364/api/blackjack?playerName=' + self.playerName();
    websocket = new WebSocket(uri);

    websocket.onopen = function () {
      self.connectionStatus("Connected");
    };

    websocket.onerror = function (event) {
      self.connectionStatus("Connection Error :(");
    }

    websocket.onmessage = function (event) {
      var dataJson = JSON.parse(event.data);
      self.game(dataJson);
    }
  }

  self.gameAction = function (actionString) {
    websocket.send(actionString);
  }

  return blackjack;
})();

ko.applyBindings(new Blackjack());