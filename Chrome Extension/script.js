var readiedPlayers = [];
var idlePlayers = [];
var playingPlayers = [];
var castingName;
var chosenEntry = 'C://test.txt';

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

////// END CHROME CALLBACKS ////////


function AnalyzeResults(result)
{
  readiedPlayers = result[0];
  idlePlayers = result[1];
  playingPlayers = result[2];
  castingName = result[3];

  // Display Readied Players
	var ReadiedList = $('#readiedPlayers');
  ReadiedList.empty();
	for (var i = 0; i < readiedPlayers.length; i++)
	{
		//ReadiedList.append('<li>' + readiedPlayers[i].id + ', ' + readiedPlayers[i].name + '</li>');
    ReadiedList.append('<li>' + readiedPlayers[i].name + '</li>');
	}

  if (readiedPlayers.length == 0)
  {
    $('#mainText').text('No Players Ready');
  }

  else
  {
    $('#mainText').text(readiedPlayers.length + ' Players are Ready');
  }

  if (readiedPlayers.length > 0 || idlePlayers.length > 0 || playingPlayers.length > 0)
  {
    $('#saveButton').show();
  }

  else
  {
    ReadiedList.append('No players found');
  }
}

function InjectAnalyzeScript() 
{
	var getText = Array();
	chrome.tabs.executeScript({file: 'analyze.js'}, function (result){AnalyzeResults(result[0]);});
}

function SaveData()
{
  var now = $.now();
  var list = now + ', ' + castingName + ',' + "\r\n";

  for (var i = 0; i < readiedPlayers.length; i++)
  {
    list = list + readiedPlayers[i].id + ', ' + readiedPlayers[i].name + ', ready' + "\r\n";
  }

  for (var i = 0; i < idlePlayers.length; i++)
  {
    list = list + idlePlayers[i].id + ', ' + idlePlayers[i].name + ', idle' + "\r\n";
  }

  for (var i = 0; i < playingPlayers.name; i++)
  {
    list = list + playingPlayers[i].id + ', ' + playingPlayers[i].name + ', in game' + "\r\n";
  }

  list = btoa(list);

  var link = document.createElement('a');
  link.download = now + '.csv';
  link.href = 'data:text/csv;base64,' + list;
  link.click();
}

document.addEventListener('DOMContentLoaded', function() {
	document.getElementById('analyzeButton').addEventListener('click', InjectAnalyzeScript);
  document.getElementById('saveButton').addEventListener('click', SaveData);
});