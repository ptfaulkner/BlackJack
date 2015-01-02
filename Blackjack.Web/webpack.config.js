module.exports = {
  // Entry point for static analyzer:
  entry: './Scripts/entry',

  output: {
    // Where to put build results when doing production builds:
    // (Server doesn't write to the disk, but this is required.)
    path: __dirname + '/Scripts',

    // JS filename you're going to use in HTML
    filename: 'bundle.js',

    // Path you're going to use in HTML
    publicPath: '/scripts/'
  },

  resolve: {
    // Allow to omit extensions when requiring these files
    extensions: ['', '.js', '.jsx']
  },

  module: {
    loaders: [
      // Pass *.jsx files through jsx-loader transform
      { test: /\.jsx$/, loader: 'jsx' }
    ]
  }
};

