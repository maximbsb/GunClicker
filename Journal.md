# 10/10/2023
Today I started working on my game Gun Clicker. The idea of the game is to shoot a gun by clicking on it, score points, reload it, upgrade it and then level up to a newer, better gun by putting the new gun's parts together. I decided to make this particular game because it mostly relies on code and isn't that reliant on animations and art. In addition, a game like this isn't overly complicated to explain in 4 tutorials, while a shooter or an RTS game would take a lot of tutorials to complete.

Firstly, I imported some guns and muzzle flashes that I can work with. I managed to create a muzzle flash system where I can select 1 out of 10 possible variations of muzzle flashes and play a random one by clicking the mouse. 

The problem that I encountered was that after an animation has ended playing, muzzle flash got frozen on the last frame and never dissapear. To solve this, I used a coroutine to wait for a 0.15 seconds and turn off the muzzle flash when the time is up. Then I realised that the solution isn't perfect because if I will want to add a muzzle flash animation that is longer than 0.15 seconds, it will stop playing midway. A better solution would be to get an animator component, get the currently playing animation in order to use its duration as a delay time.

# 17/10/2023



# 24/10/2023

