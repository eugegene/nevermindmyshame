module.exports = {
  default: {
    loader: ['ts-node/esm'],     
    import: [
      'features/support/**/*.ts'
    ],
    paths: [
      'features/**/*.feature'
    ],
    format: ['progress-bar'],
    worldParameters: {
      appUrl: 'http://localhost:5173',
    },
    formatOptions: {
      snippetInterface: 'async-await'
    },
    setTimeout: 30000
  },
};