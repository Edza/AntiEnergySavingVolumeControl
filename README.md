# AntiEnergySavingVolumeControl

My Creative T6300 speakers automatically shut down themselves if for more than 15 minutes the volume output from the PC is less than 25%. 
This is obviously a problem.

What this program does is REPLACE the windows 8/10 volume controler. The one that is called when you press volume up/volume down on your keyboard (if you have such buttons.)

This new volume controler will control the sound from 25% to 100%. But 25% will be the new 0% and 100% will remain the 100%.

This accomplishes that YOU CAN NEVER GO BELOW THE 25% threshold and your speakers WILL NOT turn off by themselves anymore.

In this new volume controler if you go to 0% the sound is muted just as if it was a real 0%.

Obviously you will need to adjust the physical volume controler on your speakers to the appropriate level so that you are comfortable with the new 25% to 100% interval.

## THIS PROJECT IS JUST STARTED, NO DEVELOPMENT YET DONE.... 
Everything great starts with an idea, working stuff coming soon

## Advanced ideas
In this setup there is still the following problem:
- You set the physical speaker volume controler to low. Now you will be able to go from 25% to 100%. But perhaps 100% is NOT loud enough for you, or the other way around.
- There actually is a solution for this. The idea is to crank up the physical controller to almost max. And then the application will have e.g. three modes:


1. First where the sound will be changed from 25% - 35%
2. 25% - 75%
3. 25% - 100%

But keep in mind that YOU will be ALWAYS changing from 0% to 100%. Just that there will be these three modes for varying degrees of loudness. This is kinda messy to implement so I'm not sure I will program this currently.
