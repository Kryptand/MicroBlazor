const MinifyPlugin = require("babel-minify-webpack-plugin");
module.exports = {
    entry: './client/storage.js',
    module: {
        rules: [
            {
                test: /\.(js)$/,
                exclude: /node_modules/,
                use: ['babel-loader']
            }
        ]
    },
    resolve: {
        extensions: ['*', '.js']
    },
    output: {
        path: __dirname + '/wwwroot',
        publicPath: '/',
        filename: 'storage.min.js'
    },
    plugins: [
        new MinifyPlugin()
    ],
    devServer: {
        contentBase: './wwwroot'
    }
};