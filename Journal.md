# 10/10/2023
I started working on my game Gun Clicker. The idea of the game is to shoot a gun by clicking on it, score points, reload it, upgrade it and then level up to a newer, better gun by putting the new gun's parts together. I decided to make this particular game because it mostly relies on code and isn't that reliant on animations and art. In addition, a game like this isn't overly complicated to explain in 4 tutorials, while a shooter or an RTS game would take a lot of tutorials to complete.

Firstly, I imported some guns and muzzle flashes that I can work with. I managed to create a muzzle flash system where I can select 1 out of 10 possible variations of muzzle flashes and play a random one by clicking the mouse. 

The problem that I encountered was that after an animation has ended playing, muzzle flash got frozen on the last frame and never dissapear. To solve this, I used a coroutine to wait for a 0.15 seconds and turn off the muzzle flash when the time is up. Then I realised that the solution isn't perfect because if I will want to add a muzzle flash animation that is longer than 0.15 seconds, it will stop playing midway. A better solution would be to get an animator component, get the currently playing animation in order to use its duration as a delay time. This method is slightly slower than directly setting a value into the delay, but it is solves the issue of a muzzle flash animation stopping before it is supposed to end, so that is the method that I ended up using.

# 17/10/2023
I made a scrollable list that will contain guns we can click on to shoot targets and earn points. I haven't encountered any problems with the list functionality, however, the issue that I encountered was that a video on my tutorial.md file was showing up as a link instead of a video player. At first, I tried to modify .md file on my computer locally. It generated a link, but not the player. The way I solved it was by openning the .md file on github.com and dragging the video file into it. This way github generates its own link, which creates a video player, just like I wanted. 

# 31/10/2023
As I was making the first tutorial, I've learnt that if I want to be able to see 3D objects in a canvas along with other 2D elements, I need to have my canvas' render mode set to `Screen Space - Camera` and select my main camera as a `Render Camera`. This way the camera can render both 3D and 2D elements together.

# 7/11/2023
As I was making a tutorial about populating a scrollable list, I found out about a super useful component called `Content Size Fitter`. Previously, I wrote a script that would do that for me, which took a lot of time. The `Content Size Fitter` allows to expand the scrollable list automatically upon adding new objects to it!

Another thing that I've learnt  was a `Reset` function, that is called once a script is attached on an object or if we right click on the script on an object and press `Reset`. This allows to automatically assign values into fields without manually having to drag it there. Here is a simple example of the code I have in my game:
```.cs
private void Reset()
{
    animator = GetComponentInChildren<Animator>();
}
```

# 14/11/2023
In case my weapon name will be too long, I was looking for a way to make my text to change dynamically in size. I found a setting in TextMeshPro text that I haven't noticed before called `Auto Size`. In the setting, you can set the minimum and maximum text size. If the text gets longer, the font will automatically get smaller so that it can fit without the `Rect Transform`.

# 28/11/2023
I learnt how to make a generic function that for my pooling system in case my pools are not of the same type. Without this generic function, I'd have to create a new function for a specific type of pool I need, which would be code repetition.
```.cs
private async Task ReleasePoolItemWithDelay<T>(ObjectPool<T> pool, T item, float delay) where T : class
{
    await Task.Delay(Mathf.RoundToInt(delay * 1000));
    pool.Release(item);
}
```
