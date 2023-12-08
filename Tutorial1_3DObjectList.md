# 3D Object Scrollable List
## This tutorial shows how to make a scrollable list that contains both 2D and 3D objects. The scrollable list will store various weapons that players will be able to tap to shoot them. Each gun will give different amount of points. Along with guns, each list element will display a gun's name, it's damage (points that it gives per shot) and a target object that will be shot at.

1. Create a new folder in your project by right-clicking on the `Assets` folder in the `Project` tab. In the popped-up window click Create -> Folder. Click twice slowly on the the new folder or select it and press `F2`. This will let you rename the folder. Name it `Scenes`.
1. Create a new scene in the `Scenes` folder by right-clicking on it, then `Create -> Scene`. Name it `GameScene`.
2. Double click on it to go to the new scene.

### Let's add a scroll view and tweak some of its settings 

1. Add a scroll view by clicking on GameObject -> UI -> Scroll View
Expand the newly added Scroll View and delete `Scrollbar Horizontal` and `Scrollbar Vertical`

2. Select the Scroll View game object. In the `Transform` component, press on the `Anchor Presets` button. Hold Alt+Shift and click on the bottom-right corner of the window to set the anchor to the bottom-right corner of the window. This will stretch the scroll view to fill the entire window.

3. Find a `Scroll Rect` component and set the `Horizontal` field to false. This will disable horizontal scrolling, which we don't need.

![AnchorPresets.png](Images%2FAnchorPresets.png)

4. Select the Scroll View game object, find the `Image` component and change the color to black by clicking on the field. Then in the newly poped-up window, set R, B, G to 0 and set A (transparency) to 1. The `Color` field should now be black like in the screenshot above.

5. Close the color picker window. Right-click on the `Source Image` field and press delete to remove the sprite from the image. It should now say `None (Sprite)`. We have to do this because the default image is rounded slightly at the corners, which we don't need in this case. By removing the image, we now have a perfect rectangle.

6. Select the Scroll View game object and set Right, Left, Top, Bottom to 0 in the `Rect Transform` component. This will stretch the scroll view to fill the entire window. On the `Image` component, clear the `Source Image` field.

![ViewportSettings.png](Images%2FViewportSettings.png)

7. Select the `Content` game object. Add a `Vertical Layout Group` component. Set the `Child Alignment` to `Upper Center`. Tick the `Control Child Size`. This will make the list of objects expand to fill the entire width of the scroll view. Add a `Content Size Fitter` component and set the same settings as shown below. This will automatically increase the height of the `Content` gameobject depending on the height of the child objects, which will allow players to scroll further. Set the `Anchor Preset` of the `Content` game object to bottom stretch while holding Alt+Shift. 

![ContentGameObject.png](Images%2FContentGameObject.png)

8. Add a few images to the `Content` game object by right-clicking on the `Content -> UI -> Image` and change their size (You will only be able to change their height since their width is controlled by the `Content Size Fitter` component on the `Content` game object) and color.

You should get something like this:

https://github.com/maximbsb/GunClicker/assets/62714778/67d53540-2f38-4153-986d-aeceb101897b

Before we can add any 3D objects inside this list, we have to make a few changes to the `Canvas` game object. 
![CanvasSettings](https://github.com/maximbsb/GunClicker/assets/62714778/1f361b35-4876-44f0-9b17-2afa553a1051)

9. It's good practice to have our canvas' `UI Scale Mode` in the `Canvas Scaler` set to `Scale With Screen Size` because this will expand or shrink our UI elements based on the screen size.
10. Set reference resolution to 1920X1080 as this is the most common screen resolution.
11. As you can see on the screenshot above, I changed the `Render Mode` in the `Canvas` component to `Screen Space - Camera`. This will ensure that our 3D object is visible when we add it to any UI Element.
12. To make sure that we can see 3D objects in the canvas now, right-click on any `image you want in the scene -> 3D Object -> Cube`. Make this cube 100 times bigger and play the game. You should see that your cube is attached to an image and as you scroll it moves with it!

In the following tutorials, we will swap this cube with a gun and add other UI elements.
