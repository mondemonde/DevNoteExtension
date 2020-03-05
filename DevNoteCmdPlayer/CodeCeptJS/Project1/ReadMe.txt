You should install codecept js here.

1.  Open cmd console in ADMIN

2. CD to your executing folder ..bin\Debug\CodeceptJs\Project1>

3.  run command ..bin\Debug\CodeceptJs\Project1>npm i codeceptjs puppeteer

3.  after puppeteer is installed run command ..CodeceptJs\Project1>npx codeceptjs init


------------------------------------------------------------------------


SAMPLE RESULTS:

D:\_MY_PROJECTS\Mond\AIFS_Manager\DevNoteCmd\CodeCeptJS\Project1>npm i codeceptjs puppeteer

> puppeteer@1.19.0 install D:\_MY_PROJECTS\Mond\AIFS_Manager\DevNoteCmd\CodeCeptJS\Project1\node_modules\puppeteer
> node install.js

Downloading Chromium r674921 - 134.9 Mb [====================] 100% 0.0s
Chromium downloaded to D:\_MY_PROJECTS\Mond\AIFS_Manager\DevNoteCmd\CodeCeptJS\Project1\node_modules\puppeteer\.local-chromium\win32-674921
npm WARN saveError ENOENT: no such file or directory, open 'D:\_MY_PROJECTS\Mond\AIFS_Manager\DevNoteCmd\CodeCeptJS\Project1\package.json'
npm notice created a lockfile as package-lock.json. You should commit this file.
npm WARN enoent ENOENT: no such file or directory, open 'D:\_MY_PROJECTS\Mond\AIFS_Manager\DevNoteCmd\CodeCeptJS\Project1\package.json'
npm WARN Project1 No description
npm WARN Project1 No repository field.
npm WARN Project1 No README data
npm WARN Project1 No license field.

+ codeceptjs@2.2.0
+ puppeteer@1.19.0
added 194 packages from 410 contributors and audited 395 packages in 161.491s
found 0 vulnerabilities


D:\_MY_PROJECTS\Mond\AIFS_Manager\DevNoteCmd\CodeCeptJS\Project1>
D:\_MY_PROJECTS\Mond\AIFS_Manager\DevNoteCmd\CodeCeptJS\Project1>npx codeceptjs init

  Welcome to CodeceptJS initialization tool
  It will prepare and configure a test environment for you

Installing to D:\_MY_PROJECTS\Mond\AIFS_Manager\DevNoteCmd\CodeCeptJS\Project1
? Where are your tests located? ./*_test.js
? What helpers do you want to use? Puppeteer
? Where should logs, screenshots, and reports to be stored? ./output
? Would you like to extend the "I" object with custom steps? Yes
? Where would you like to place custom steps? ./steps_file.js
? Do you want to choose localization for tests? English (no localization)
Configure helpers...
? [Puppeteer] Base url of site to be tested http://localhost
Steps file created at D:\_MY_PROJECTS\Mond\AIFS_Manager\DevNoteCmd\CodeCeptJS\Project1\steps_file.js
TypeScript project configuration file created at D:\_MY_PROJECTS\Mond\AIFS_Manager\DevNoteCmd\CodeCeptJS\Project1\tsconfig.json
Config created at D:\_MY_PROJECTS\Mond\AIFS_Manager\DevNoteCmd\CodeCeptJS\Project1\codecept.conf.js
Directory for temporary output files created at './output'
Almost done! Create your first test by executing `npx codeceptjs gt` (generate test) command

D:\_MY_PROJECTS\Mond\AIFS_Manager\DevNoteCmd\CodeCeptJS\Project1>
