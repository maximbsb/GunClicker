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

7. Copy the "Transform" Component of the gun by right-clicking on the "Transform" component  of the gun Copy -> Component.
8. Right-click on the "GunCell" GameObject and select "Create empty". Name it "Gun Transform".
9. Select the "Gun Transform" and copy the position, rotation and scale values of the gun GameObject over to it. This will save the gun's position, rotation and scale for the time when we spawn it into the scene via code.
10. Delete the gun by selecting it and pressing "Delete" key.
11. Create a new folder and name it "Prefabs".
12. Drag the "GunCell" into the "Prefabs" folder. This will create a prefab of the GameObject. By making it a prefab, we can spawn it into the scene multiple times via code, instead of spawning each child of "GunCell" manually and tweaking values of every component. 

## 3. Creating a Gun Cell Script
The purpose of the "GunCell" script is to assign text values of the gun name and damage and to spawn a correct gun model according to a passed in gun ScriptableObject.

```.cs
public class GunCell : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private TMP_Text gunNameText;
    [SerializeField] private TMP_Text gunDamageText;
    [SerializeField] private Transform gunTransform;
   
    public void Init(GunSO gunSO)
    {
        gunNameText.text = gunSO.name;
        gunDamageText.text = gunSO.damage.ToString("F0");
        Instantiate(gunSO.prefab, gunTransform.position, gunTransform.rotation, gunTransform);
    }
```
Attach the "GunCell" script to the newly created prefab by selecting the "GunCell" prefab in the "Prefabs" folder, scrolling all the way down in the "Inspector" tab, pressing "Add Component" and searching for the "GunCell" script. Double-click on the "GunCell" prefab and drag "Gun Name" GameObject into the "Gun Name Text" field, "Gun Damage" into "Gun Damage Text" field and "Gun Transform" into the "Gun Transform" field.
![GunCellPrefab](https://github.com/maximbsb/GunClicker/assets/62714778/837797a7-da20-4c7f-ad50-c0992be97ddf)

Press this button to return to the "Scene" view:
![image](https://github.com/maximbsb/GunClicker/assets/62714778/23b83117-9b4e-4fe8-be0c-245bb2e0b70b)
