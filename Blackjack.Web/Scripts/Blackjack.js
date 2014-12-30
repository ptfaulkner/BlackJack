var Blackjack = (function () {
  var self = this,
    websocket;

  var blackjack = function () {
    self.game = ko.observable();
    self.connectionStatus = ko.observable('Not Connected');
    self.playerName = ko.observable();
  };

  self.connect = function () {
    var host = window.location.host,
      protocol = window.location.protocol,
      uri = (protocol === 'https:' ? 'wss' : 'ws') + '://' + host + '/api/blackjack?playerName=' + self.playerName();
    websocket = new WebSocket(uri);

    websocket.onopen = function () {
      self.connectionStatus('Connected');
    };

    websocket.onerror = function (event) {
      self.connectionStatus('Connection Error :(');
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