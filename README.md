Pikatwo 1.1.0
=========
Pikatwo is an IRC-enabled chat bot that logs IRC chatter, and uses it to construct a log full of commentary. The bot will then regurgitate this commentary in a hopefully-amusing fashion when someone speaks to it on IRC.

This release contains the Pikatwo binary along with 42MB of response data. Since the response data was collected by an automated script, it may be vulgar in nature. The response data is not on github, and is only present in the release. If you wish to parse your own response data, you'll have to compile from source since the response data parser is not included in the release.

IF YOU WANT TO COMPILE FROM SOURCE:

You can grab the dependency binaries in the github release under "releases" section. The dependencies are:

log4net.dll

HtmlAgilityPack.dll

Meebey.SmartIRc4net.dll

Newtonsoft.Json.dll

StarkSoftProxy.dll

This project is licensed under the Mozilla Public Licence 2.0.
http://www.mozilla.org/MPL/2.0/index.txt

Changelog
=========

1.1.0:

-Fixed handful of bugs having to do with github tracker announcements

-Added "!tell" phrase to hash banlist

-Added snarky ctcp reply that's broken thanks to meeblyirc4.net being crap

-Added .help function

-Drastically reduced timeout for github rss feed requests, which in turn fixed a lot of lag issues

1.0.0:

-Release

=========
Benjamin Samuels
contact@bsamuels.net


[![githalytics.com alpha](https://cruel-carlota.pagodabox.com/bb50ff8874db722315a9ca4c8cab03f2 "githalytics.com")](http://githalytics.com/bsamuels453/Pikatwo)
