<Target Name="OptimizeJs">
    <Exec Command=".bin\node Scripts\r.js -o Scripts\build.js" />
</Target>

const express = require('express');
const app = express();
const router = express.Router();


// Parse URL-encoded bodies (as sent by HTML forms)
app.use(express.urlencoded());

// Parse JSON bodies (as sent by API clients)
app.use(express.json());

app.get('/', function (request, response) {
    response.sendFile('index.html', { root: __dirname });
});


app.post("/data", (req, res) => {
    console.log(req.body);
    //res.send("About this wiki");
});

app.listen(3000, () => {
    console.log('server started');
});


module.exports = router;
//the above sets up node.js to send data in a post request