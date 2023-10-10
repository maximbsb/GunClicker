# 10/10/2023

Today I started working on my game Gun Clicker. The idea of the game is to shoot a gun by clicking on it, score points, reload it, upgrade it and then level up to a newer, better gun by putting the new gun's parts together. I decided to make this particular game because it mostly relies on code and isn't that reliant on animations and art.

I imported some guns and muzzle flashes that I can work with. I managed to create a muzzle flash system where I can select 1 out of 10 possible variations of muzzle flashes and play a random one by clicking the mouse. The problem that I encountered was that after an animation has ended playing, muzzle flash got frozen on the last frame and never dissapear. To solve this, I used a coroutine to wait for a 0.15 seconds and turn off the muzzle flash when the time is up.
