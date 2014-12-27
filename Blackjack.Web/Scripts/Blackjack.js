var Blackjack = (function () {
  var self = this,
    websocket;


  var blackjack = function (game) {
    self.game = game || {};
    self.connectionStatus = ko.observable("Not Connected");

    self.connect();
  };

  self.connect = function () {
    var uri = 'ws://localhost:51364/api/blackjack';
    websocket = new WebSocket(uri);

    websocket.onopen = function () {
      self.connectionStatus("Connected");
    };

    websocket.onerror = function (event) {
      self.connectionStatus("Connection Error :(");
    }

    websocket.onmessage = function (event) {

    }
  }

  self.send = function () {
    websocket.send("some data");
  }

  return blackjack;
})();

ko.applyBindings(new Blackjack());