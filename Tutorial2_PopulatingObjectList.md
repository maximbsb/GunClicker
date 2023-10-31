# Populating Weapon List
## In this tutorial, we will populate our scrollable list with guns, their names and damage values. To be ready for this tutorial, please download and import a unity package with guns. It can be any package you want. It's recommended that you have at least 2 guns in the pack for this tutorial.

## 1. Storing Gun Data
1. Create a new folder and name it "Scripts".
2. Create a script in the `Scripts` folder by right-clicking on the folder Create -> C# Script and call it `GunSO`.

```.cs
[CreateAssetMenu(fileName = "Gun", menuName = "New Gun", order = 0)]
public class GunSO : ScriptableObject
{
    public string name;
    public GameObject prefab;
    public float damage;
}
```
This script is not going to inherit from MonoBehaviour, but rather from a ScriptableObject. ScriptableObjects are used for storing large quantities of data. The main purpose of ScriptableObjects is to reduce memory usage within the project by avoiding copies of values. We cannot attach ScriptableObjects to a GameObject, instead, we need to save them as assets in the project. To be able to create assets, we added an attribute about the `GunSO` class called `CreateAssetMenu`, which allows us to add a button to the `Create` list of options that you saw when adding folders and this script. Inside the class, we added 3 variables that we will use to store important data for each gun.

3. Copy the code above into your script (without removing your using statements at the very top of the script) and save it.
4. Create a new folder called "GunStats". This folder will contain ScriptableObject assets with gun data.
5. Select the "GunStats" folder and right-click in the empty space, then select Create -> New Gun. Name the newly created asset however you like.
6. Select the asset and fill in the `name` and `damage` values inside it.
  
![GunSOFirstGun](https://github.com/maximbsb/GunClicker/assets/62714778/bb29111b-f743-47df-911a-0d5e8afb93f3)

Later we will create more ScriptableObject assets for other guns we will add.

## 2. Creating a Gun Cell Prefab
1. Add an image to the "Content" GameObject and name it "GunCell". Change the color of the image to any color you want.
2. Add a gun as a child object of the "GunCell" GameObject, scale it (by a lot) and place it in the left part of the image. Make sure that it is not behind the UI, otherwise you will not able to see it.
3. Right-click on the "GunCell" GameObject and select UI->Text-TextMeshPro. Once you press it, you will have a pop up asking to import TMP essentials. Press "Import TMP Essentials" and close the window. You should see a text GameObject was added as a child of "GunCell". Call it "Gun Name".
4. Change settings of a "Gun Name" GameObject as shown on the screenshot below. Here we change the width, height, PosX and PosY of the "Rect Transform" component to place the text in a good spot. Then we change the text itself to say "Gun Name" we tick a check box called "Auto Size" which will ensure that our text will not escape the text box by reducing in size if our gun name is too long.

![GunNameText](https://github.com/maximbsb/GunClicker/assets/62714778/70cccee5-b65c-4cf5-97e5-22067512c4a5)

6. Add another TMP Text to the "GunCell" GameObject and call it "Gun Damage". Place the text below the gun and change the color of the text to red.

Now your cell should look something like in the screenshot above.

