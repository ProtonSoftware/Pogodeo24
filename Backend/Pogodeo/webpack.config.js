  const path = require('path');

  module.exports = {
	entry: './wwwroot/js/Main.js',
    output: {
      filename: 'app.bundle.js',
      path: path.resolve(__dirname, 'wwwroot/dist')
	},
	mode: 'production',
	module: {
     rules: [
       {
         test: /\.css$/,
         use: [
           'style-loader',
           'css-loader'
         ]
       },
	   {
			test: /\.(png|jpg|gif|svg|eot|ttf|woff|woff2|json|xml|ico)$/,
			loader: 'file-loader',
			query: {
				outputPath: 'assets/',
				publicPath: 'http://localhost:8080/',
				emitFile: true
			}
		}
     ]
   }
}