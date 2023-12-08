# 10/10/2023
I started working on my game Gun Clicker. The idea of the game is to shoot a gun by clicking on it, score points, reload it, upgrade it and then level up to a newer, better gun by putting the new gun's parts together. I decided to make this particular game because it mostly relies on code and isn't that reliant on animations and art. In addition, a game like this isn't overly complicated to explain in 4 tutorials, while a shooter or an RTS game would take a lot of tutorials to complete.

Firstly, I imported some guns and muzzle flashes that I can work with. I managed to create a muzzle flash system where I can select 1 out of 10 possible variations of muzzle flashes and play a random one by clicking the mouse. 

The problem that I encountered was that after an animation has ended playing, muzzle flash got frozen on the last frame and never dissapear. To solve this, I used a coroutine to wait for a 0.15 seconds and turn off the muzzle flash when the time is up. Then I realised that the solution isn't perfect because if I will want to add a muzzle flash animation that is longer than 0.15 seconds, it will stop playing midway. A better solution would be to get an animator component, get the currently playing animation in order to use its duration as a delay time. This method is slightly slower than directly setting a value into the delay, but it is solves the issue of a muzzle flash animation stopping before it is supposed to end, so that is the method that I ended up using.

# 17/10/2023
I made a scrollable list that will contain guns we can click on to shoot targets and earn points. I haven't encountered any problems with the list functionality, however, the issue that I encountered was that a video on my tutorial.md file was showing up as a link instead of a video player. At first, I tried to modify .md file on my computer locally. It generated a link, but not the player. The way I solved it was by openning the .md file on github.com and dragging the video file into it. This way github generates its own link, which creates a video player, just like I wanted. The alternative would be to have all the video files stored in another github folder, which then the user can access himself, but this solution is a lot less convenient for the user.

# 31/10/2023
A problem that I encountered today was that 3D objects were not being displayed inside my scrollable view, which is a UI element inside a canvas. To put my guns inside so that they could scroll along with the UI elements.

One solution that I thought of was have the guns outside of the canvas in front of the camera. When I scroll the list, the guns would move by the same amount as the list, however, this solution could cause various gameplay problems including list and gun de-synchronisation and other visual bugs.

Another solution that I came up with is to get rid of my scrollable list view that I am creating and make a scrollable list in 3d space outside of the canvas and use 3D text instead of a 2D text. This would be a time consuming solution, but it would work. It would also require to come up with a different way to add other UI elements such as a `Button` component, which only works on UI objects that are marked as `Raycast Target`.

The best solution was to have my canvas' render mode set to `Screen Space - Camera` and select my main camera as a `Render Camera`. This way the camera can render both 3D and 2D elements together! This way I could keep what I've done previously, and achieve the wanted effect with minimum effort.

# 7/11/2023
Upon adding new items to a scrollable list at runtime, I realised that my list doesn't scroll all the way to the last item. It happened because my `Content` object which contains all the list items didn't increase in height. 

At first, I thought that the best solution would be to make a script that would grow the list's height based on the amount of items that it has. For example, 1 item = 200, 2 items = 400 etc. This way would work, but if my item size is not constant, the script would definetely require more work.

Instead, after some research, I found out about a super useful component called `Content Size Fitter`. The `Content Size Fitter` allows to expand the scrollable list automatically upon adding new objects to it! This solved my problem immedeately and this method works even with items of different sizes.

Another thing that I've learnt  was a `Reset` function, that is called once a script is attached on an object or if we right click on the script on an object and press `Reset`. This allows to automatically assign values into fields without manually having to drag it there. Here is a simple example of the code I have in my game:
```.cs
private void Reset()
{
    animator = GetComponentInChildren<Animator>();
}
```

# 14/11/2023
A small problem that I encountered today was that my text was going outside of the text box when there are too many characters. It could fit enough characters if I made it smaller, however, it was very difficult to see and it looked weird with text that is small.

The first solution could be to write a script that would make the font become depended on an amount of characters in a text. 10 characters = font 50, 30 characters = font 20, for example. However, this would take  time to come up with a script that works properly.

In the end, I found a setting in TextMeshPro text that I haven't noticed before called `Auto Size`. In the setting, you can set the minimum and maximum text size. If the text gets longer, the font will automatically get smaller so that it can fit without the `Rect Transform`. This solution is a lot better than the previous one because it takes the size of the text box into account and makes it fit perfectly within the set bounds.

# 28/11/2023
In my game, I have `ObjectPool` class variables of different types. One is `ObjectPool<GameObject>` and another is `ObjectPool<ParticleSystem>`. In order to release an item back into a pool of its type, used 2 different functions called `ReleasePoolItemWithDelayGO` and `ReleasePoolItemWithDelayPS` depending on an item type that I am releasing. This solution works well, however, I realised that all the code in these functions was pretty much the same.

To avoid code repetition, I used a generic function that can be used for both `ObjectPool<GameObject>` and `ObjectPool<ParticleSystem>` types:
```.cs
private async Task ReleasePoolItemWithDelay<T>(ObjectPool<T> pool, T item, float delay) where T : class
{
    await Task.Delay(Mathf.RoundToInt(delay * 1000));
    pool.Release(item);
}
```
This way my code isn't repeated and if I ever create a new pool of yet another time, I won't have to create a third function for that specific type!
