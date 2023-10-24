# 10/10/2023
I started working on my game Gun Clicker. The idea of the game is to shoot a gun by clicking on it, score points, reload it, upgrade it and then level up to a newer, better gun by putting the new gun's parts together. I decided to make this particular game because it mostly relies on code and isn't that reliant on animations and art. In addition, a game like this isn't overly complicated to explain in 4 tutorials, while a shooter or an RTS game would take a lot of tutorials to complete.

Firstly, I imported some guns and muzzle flashes that I can work with. I managed to create a muzzle flash system where I can select 1 out of 10 possible variations of muzzle flashes and play a random one by clicking the mouse. 

The problem that I encountered was that after an animation has ended playing, muzzle flash got frozen on the last frame and never dissapear. To solve this, I used a coroutine to wait for a 0.15 seconds and turn off the muzzle flash when the time is up. Then I realised that the solution isn't perfect because if I will want to add a muzzle flash animation that is longer than 0.15 seconds, it will stop playing midway. A better solution would be to get an animator component, get the currently playing animation in order to use its duration as a delay time. This method is slightly slower than directly setting a value into the delay, but it is solves the issue of a muzzle flass animation stopping before it is supposed to end.

# 17/10/2023
I made a scrollable list that will contain guns we can click on to shoot targets and earn points. I haven't encountered any problems with the list functionality, however, the issue that I encountered was that a video on my tutorial.md file was showing up as a link instead of a video player. At first, I tried to modify .md file on my computer locally. It generated a link, but not the player. The way I solved it was by openning the .md file on github.com and dragging the video file into it. This way github generates its own link, which creates a video player, just like I wanted. 


# 24/10/2023

