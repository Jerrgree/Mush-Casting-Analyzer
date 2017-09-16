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
  var readiedPlayers = result[0];
  var idlePlayers = result[1];
  var playingPlayers = result[2];

  // Display Readied Players
	var ReadiedList = $('#readiedPlayers');
  ReadiedList.empty();
  ReadiedList.append('Readied Players');
	for (var i = 0; i < readiedPlayers.length; i++)
	{
		ReadiedList.append('<li>' + readiedPlayers[i].id + ', ' + readiedPlayers[i].name + '</li>');
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