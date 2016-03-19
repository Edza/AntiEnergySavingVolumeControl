# Install instructions
1. Find the "Release" button in the middle of the screen (Use Ctrl + F if you cant find it LOL)
2. Download the .EXE file
3. Set up the .EXE file to run as everytime the computer starts
4. Restart and your done! ;)

# How do I setup it runs every time I start my PC?
1. Click "Search" and type in "Task Scheduler"
2. Open it. Click on Task Scheduler Library
3. Left click in the list "Create a new  task"
4. Type in the name of the task "My volume controller"
5. Check "Run with higest priviliges" (you can skip this, I never tested without it, it probably works fine)
6. Dropdown "Configure for" choose "Windows 10"
7. Triggers tab, new trigger, first dropdown "At startup"
8. Check delay task for "1 minute" (you need this)
9. Go to Actions, new action, Chose to start a program, select the CustomVolumeControler.exe
10. Go to Settings, uncheck "Stop the task if is running longer than 3 days"
*11. Recheck everything and congratualte yourself - you have just learned something new about Windows!*

## Whats the problem?
My Creative T6300 speakers automatically shut down themselves if for more than 15 minutes the volume output from the PC is less than 25%. 
This is obviously a problem.

What this program does is REPLACE the windows 8/10 volume controler. The one that is called when you press volume up/volume down on your keyboard (if you have such buttons.)

This new volume controler will control the sound from 25% to 100%. You can go below 25% if you wish, but you will have to hold down the volume down key for 3 seconds.

Mute key works as expected

You will need to run this application on windows startup. I use a scheduled task with admin right, and a 1 minute delay. There seems to be some race condition with the built in audio controler, but this configuration fixes it.


