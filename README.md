# AntiEnergySavingVolumeControl

My Creative T6300 speakers automatically shut down themselves if for more than 15 minutes the volume output from the PC is less than 25%. 
This is obviously a problem.

What this program does is REPLACE the windows 8/10 volume controler. The one that is called when you press volume up/volume down on your keyboard (if you have such buttons.)

This new volume controler will control the sound from 25% to 100%. You can go below 25% if you wish, but you will have to hold down the volume down key for 3 seconds.

Mute key works as expected

You will need to run this application on windows startup. I use a scheduled task with admin right, and a 1 minute delay. There seems to be some race condition with the built in audio controler, but this configuration fixes it.


