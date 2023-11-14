# Shooting System
## In this tutorial, we will make a gun shooting system, where upon clicking on a gun, the gun will play a recoil animation, spawn a muzzle flash, and shoot a bullet into the target. Based on the damage of a gun, points will be given to the player.

## 1. Displaying points to the player
1. Create a new script in the "Scripts" folder and name it 'Currency'

```.cs
public class Currency : MonoBehaviour
{
    [SerializeField] private float currency;
    public event Action OnCurrencyChanged;
    
    public float GetCurrency()
    {
        return currency;
    }
    
    public void AddCurrency(float amount)
    {
        if(amount < 0)
            throw new Exception("Cannot add negative currency");
        currency += amount;  
        OnCurrencyChanged?.Invoke();
    }
    
    public void SpendCurrency(float amount)
    {
        if(amount < 0)
            throw new Exception("Cannot spend negative currency");
        
        if (currency >= amount)
        {
            currency -= amount;
            OnCurrencyChanged?.Invoke();
        }
    }
```
Explanation:
'[SerializeField] private float currency' is a variable that will store our points. We made it private so that it can't be directly influenced by other scripts. I also added an attribute called 'SerializeField', which allows us to show the variable in the inspector and modify it from there.

'public event Action OnCurrencyChanged' is an event that will be be invoked everytime that the currecy value will be changed using either function 'AddCurrency' or 'SpendCurrency'. This will be used to update a UI Text when necessary.

2. Copy the code above into your script (without removing your using statements at the very top of the script) and save it.
3. After your editor has finished compiling, create a new empty gameobject and call it 'Currency'.
4. Add the 'Currency' script onto the 'Currency' gameobject.
5. Create a new script in the "Scripts" folder and name it 'CurrencyDisplay'

```.cs
public class CurrencyDisplay : MonoBehaviour
{
    [SerializeField] private Currency currency;
    [SerializeField] private TMP_Text currencyText;

    private void Start()
    {
        currency.OnCurrencyChanged += UpdateText;
        UpdateText();
    }

    private void UpdateText()
    {
        currencyText.text = currency.GetCurrency().ToString("F0");
    }

    private void Reset()
    {
        currency = FindObjectOfType<Currency>();
        currencyText = GetComponent<TMP_Text>();
    }
}    
```
Explanation: 
On 'Start' we subscribe the function called 'UpdateText' to the 'OnCurrencyChanged' event in the 'Currency' class. This means that everytime that we invoke this event, 'UpdateText" will be called and the text will be updated. We could update our text in the 'Update' callback function, however, that would mean that we would do unnecessary computations every frame, even though we aren't doing anything to the text. Using events allows know exactly when the currency text should be updated and saves us some frame time.
In the 'UpdateText' function, we call a 'GetCurrency' function which acts as the getter for our private 'currency' variable and then we call 'ToString' with an "F0" attribute inside it, which converts our float value to string and makes it have 0 decimal places. If we would put "F1", our string would have 1 decimal place.
The 'Reset' function is called when we first attach a script to a gameobject. This means that it will try to find an object with a 'Currency' component and put it in its 'currency' variable and it will get a text component if it's present on this gameobject. This speeds up the workflow since we don't have to manually drag the components to their variable fields.

6. Create a new TMP_Text as a child of a "Canvas" gameobject and name it 'CurrencyText'. Anchor it to the top of the screen and make it larger if you have to.
7. Attach the 'CurrencyDisplay' to the 'CurrencyText' gameobject. Make sure that all the fields contain an appropriate value.
8. Go to the 'Currency' gameobject and change the value of the 'currency' field to any other number you want. I will set mine to 369.
   ![image](https://github.com/maximbsb/GunClicker/assets/62714778/dec33249-6cd9-4010-b250-6b3669958f33)
As you can see, now our currency text updates with our 'currency' field. However, if you change the value of the 'currency' field in runtime, you will see that the text will not change! This is because we only update our currency text when the 'OnChangeCurrency' event is invoked! Later on in the tutorial, you will see that when we call our 'AddCurrency' and 'SpendCurrency' functions, the text will update when necessary.
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
        Instantiate(gunSO.prefab, gunTransform);
    }
```
Attach the "GunCell" script to the newly created prefab by selecting the "GunCell" prefab in the "Prefabs" folder, scrolling all the way down in the "Inspector" tab, pressing "Add Component" and searching for the "GunCell" script. Double-click on the "GunCell" prefab and drag "Gun Name" GameObject into the "Gun Name Text" field, "Gun Damage" into "Gun Damage Text" field and "Gun Transform" into the "Gun Transform" field.
![GunCellPrefab](https://github.com/maximbsb/GunClicker/assets/62714778/837797a7-da20-4c7f-ad50-c0992be97ddf)

Press this button to return to the "Scene" view:

![image](https://github.com/maximbsb/GunClicker/assets/62714778/23b83117-9b4e-4fe8-be0c-245bb2e0b70b)

## 4. Creating a Gun Cell Spawner Script
This script will spawn our "GunCell" prefab into the scrollable list when the game begins.

```.cs
public class GunCellSpawner : MonoBehaviour
{
    [SerializeField] private GameObject gunCellPrefab;
    [SerializeField] private List<GunSO> guns;
    
    private void Start()
    {
        foreach (var gun in guns)
        {
            GameObject gunCellGO = Instantiate(gunCellPrefab, transform);
            GunCell gunCell = gunCellGO.GetComponent<GunCell>();
            gunCell.Init(gun);
        }
    }
}
```
In the Start function, we iterate over all the Gun ScriptableObjects inside the list which we will manually populate with gun ScriptableObjects. For every gun, we create a "GunCell" prefab as a child of a GameObject that has this script. Then we will get a "GunCell" script and call Init function in the "GunCell" script which will change the text and spawn a correct gun into the cell.

Attach this script to the "Content" GameObject. Drag the "GunCell" prefab from the "Prefabs" folder into the "Gun Cell Prefab" field of the GunCellSpawner.
Add a new element by clicking a plus icon below the list called "Guns". Then drag and drop a gun ScriptableObject that we created in the "GunStats" folder.

Now if you delete all the children objects of the "Content" GameObject and start the game, you should see that our text has been set to the data set in the gun ScripableObject and a gun is spawned where it should in the gun cell!

![image](https://github.com/maximbsb/GunClicker/assets/62714778/3a3dc0d5-dfda-459f-bf73-417f4599505e)

Now create more ScriptableObjects and add them into the "Guns" list in the "GunCellSpawner" script. When you start the game once again, you should see more guns are added into your list! 

https://github.com/maximbsb/GunClicker/assets/62714778/fd0f883c-951a-40eb-b38a-d12ea8c9e499

In the next tutorial we will cover shooting guns and adding points that we get per shot.
