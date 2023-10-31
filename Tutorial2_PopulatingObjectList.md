# Populating Weapon List
In this tutorial, we will populate our scrollable list with guns, their names and damage values.

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

## 2. Creating a Cell Prefab
