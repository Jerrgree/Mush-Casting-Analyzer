function getCurrentTabUrl(callback) 
{
  var queryInfo = {
    active: true,
    currentWindow: true
  };

  chrome.tabs.query(queryInfo, (tabs) => {
    var tab = tabs[0];

    var url = tab.url;

    console.assert(typeof url == 'string', 'tab.url should be a string');

    callback(url);
  });
}

function AnalyzeResults(result)
{
	var ReadiedList = $('#readiedPlayers');

	for (var i = 0; i < result.length; i++)
	{
		console.log(result[i]);
		ReadiedList.append('<li>' + result[i].id + ', ' + result[i].name + '</li>');
	}
}

function InjectScript() 
{
	var getText = Array();
	chrome.tabs.executeScript({file: 'analyze.js'}, function (result){ console.log(result);AnalyzeResults(result[0]);});
}

document.addEventListener('DOMContentLoaded', function() {
	document.getElementById('analyzeButton').addEventListener('click', InjectScript);
});