# 3D Object Scrollable List
This shows how to make a scrollable list of 3D objects that spawn when the game begins.

## 1. Setup the scroll list functionality

Start by creating a new scene called `GameScene`.

Add a scroll view by clicking on GameObject -> UI -> Scroll View
Expand the newly added Scroll View and delete "Scrollbar Horizontal" and "Scrollbar Vertical"

Select the Scroll View game object. In the "Transform" component, press on the "Anchor Presets" button. Hold Alt+Shift and click on the bottom-right corner of the window to set the anchor to the bottom-right corner of the window. This will stretch the scroll view to fill the entire window.

Find a "Scroll Rect" component and set the "Horizontal" field to false. This will disable scrolling horizontal scrolling.

![AnchorPresets.png](Images%2FAnchorPresets.png)

Select the Scroll View game object, find the "Image" component and change the color to black by clicking on the field. Then in the newly poped-up window, set R, B, G to 0 and set A (transparency) to 1. The "Color" field should now be black like in the screenshot above.

Close the color picker window. Right-click on the "Source Image" field and press delete to remove the sprite from the image. It should now say "None (Sprite)".

Select the Scroll View game object and set Right, Left, Top, Bottom to 0 in the "Rect Transform" component. This will stretch the scroll view to fill the entire window. On the "Image" component, clear the "Source Image" field.

![ViewportSettings.png](Images%2FViewportSettings.png)

Select the "Content" game object. Add a "Vertical Layout Group" component. Set the "Child Alignment" to "Upper Center". Tick the "Control Child Size". This will make the list of objects expand to fill the entire width of the scroll view. Add a "Content Size Fitter" component and set the same settings as shown below. This will automatically increase the height depending on the height of the child objects. Set the "Anchor Preset" of the "Content" game object to bottom stretch while holding Alt+Shift. 

![ContentGameObject.png](Images%2FContentGameObject.png)

If you try adding a few images to the "Content" game object by right-clicking on the "Content" -> UI -> Image and change its size and color, you should get something like this:

https://github.com/maximbsb/GunClicker/assets/62714778/67d53540-2f38-4153-986d-aeceb101897b

Before we can add any 3D objects inside this list, we have to make a few changes to the "Canvas" gameobject. 
![CanvasSettings](https://github.com/maximbsb/GunClicker/assets/62714778/1f361b35-4876-44f0-9b17-2afa553a1051)

Firstly, It's good practice to have our canvas' "UI Scale Mode" in the "Canvas Scaler" set to "Scale With Screen Size". This will expand or shrink our UI elements based on the screen size. Set reference resolution to 1920X1080. As you can see on the screenshot above, I changed the "Render Mode" in the "Canvas" component to "Screen Space - Camera". This will ensure that our 3D object is visible when we add it to any UI Element. Right-click on any image you want in the scene -> 3D Object -> Cube. Make this cube 100 times bigger play the game. You should see that your cube is attached to an image and as you scroll it moves with it! In the following tutorials, we will change this cube with a gun.

## 2. Creating list elements in runtime 



Example of how to make a code area:
```.cs
[RequireComponent(typeof(SphereCollider))]
public class Vision : MonoBehaviour
{
    public SphereCollider viewDistance;
}
```
