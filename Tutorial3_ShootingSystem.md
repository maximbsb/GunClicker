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
On 'Start' we subscribe the function called 'UpdateText' to the 'OnCurrencyChanged' event in the 'Currency' class. This means that everytime that we invoke this event, 'UpdateText" will be called and the text will be updated. We could update our text in the 'Update' callback function, however, that would mean that we would do unnecessary computations every frame, even though we aren't doing anything to the text. Using events allows us to know exactly when the currency text should be updated and gives us some additional performance.
In the 'UpdateText' function, we call a 'GetCurrency' function which acts as the getter for our private 'currency' variable and then we call 'ToString' with an "F0" attribute inside it, which converts our float value to string and makes it have 0 decimal places. If we would put "F1", our string would have 1 decimal place.
The 'Reset' function is called when we first attach a script to a gameobject. This means that it will try to find an object with a 'Currency' component and put it in its 'currency' variable and it will get a text component if it's present on this gameobject. This speeds up the workflow since we don't have to manually drag the components to their variable fields.

6. Create a new TMP_Text as a child of a "Canvas" gameobject and name it 'CurrencyText'. Anchor it to the top of the screen and make it larger if you have to.
7. Attach the 'CurrencyDisplay' to the 'CurrencyText' gameobject. Make sure that all the fields contain an appropriate value.
8. Go to the 'Currency' gameobject and change the value of the 'currency' field to any other number you want. I will set mine to 369.
   ![image](https://github.com/maximbsb/GunClicker/assets/62714778/dec33249-6cd9-4010-b250-6b3669958f33)
As you can see, now our currency text updates with our 'currency' field. However, if you change the value of the 'currency' field in runtime, you will see that the text will not change! This is because we only update our currency text when the 'OnChangeCurrency' event is invoked! Later on in the tutorial, you will see that when we call our 'AddCurrency' and 'SpendCurrency' functions, the text will update when necessary.

## 2. Adding score when pressing on a gun
1. Create a new script in the "Scripts" folder and name it 'GunShooter'

```.cs
public class GunShooter : MonoBehaviour
{
    private GunSO gun;
    private Currency currency;
    
    public void Init(GunSO gun, Currency currency)
    {
        this.gun = gun;
        this.currency = currency;
    }

    public GunSO GetStats()
    {
        return gun;
    }

    public void Shoot()
    {
        currency.AddCurrency(gun.damage);
    }

    private void OnMouseDown()
    {
        Shoot();
    }
}
```
Explanation:
The GunShooter script will be added onto each individual gun. We added a callback function called `OnMouseDown` which gets called when we click on a collider of a gun with a mouse button or a finger. Once we call it, a `Shoot()` function will be called, where we add points to the `currency` value based on the damage of the gun that we set in the GunSO.
As you can see, the variables in this script are private and do not have a `[SerializeField]` attribute, which means that we can't assign them values in the inspector. In order to give these variables a value, I made an `Init` function which will be called from the class that spawns our gun, which is `GunCell`. We could get the values by using `FindObjectOfType` for the currency and add a `[SerializeField]` to the gun variable, however, it is more efficient to pass it down from the spawner class and saves us some time assigning GunSOs for every gun prefab. 

2. Open you `GunCell` script and modify the `Init` function:
   ```.cs
public void Init(GunSO gunSO, Currency currency)
    {
        gunNameText.text = gunSO.name;
        gunDamageText.text = gunSO.damage.ToString("F0");
        
        GameObject gunGO = Instantiate(gunSO.prefab, gunTransform);
        if (gunGO.TryGetComponent(out GunShooter gunShooter))
        {
            gunShooter.Init(gunSO, currency);
        }
        else
        {
            Debug.LogError("Gun prefab does not have GunShooter component");
        }
    }
   ```
As you can see, we added a new attribute of type `Currency`. We also save an instantiated gameobject into a variable called `gunGO`. After that, we check that this gameobject has a `GunShooter` component attached to it. If it does, we call the `Init` function to fill in all the variables if it doesn't, we give an error.

Now we have to modify the `GunCellSpawner` to have a pass the `currency` into the `Init` function:

```.cs
public class GunCellSpawner : MonoBehaviour
{
    [SerializeField] private GameObject gunCellPrefab;
    [SerializeField] private List<GunSO> guns;
    [SerializeField] private Currency currency;
    
    private void Start()
    {
        foreach (var gun in guns)
        {
            GameObject gunCellGO = Instantiate(gunCellPrefab, transform);
            GunCell gunCell = gunCellGO.GetComponent<GunCell>();
            gunCell.Init(gun,currency);
        }
    }
}
```
Here we add a new `currency` variable that we will set in the inspector. The reason why we didn't make `currency` be available in the editor for the `GunCell` and `GunShooter` is because we are spawning these gameobjects in runtime, which means that we couldn't assign a value for `currency` (We cannot assign an object in the scene to a field in a prefab of a `GunCell` or other gun prefabs).

Now that the code is done, add the `GunShooter` script on all the gun prefabs that you have. Don't forget to assign a value to a 'currency' field in the `GunCellSpawner` script that is on the `Container` gameobject! Right now, if you start the game and click on the weapons, nothing is going to happen. This is because our guns are missing a collider that is used for the detection of a mouse click. Add a box collider to a prefab of every gun that you have. 

Now if you play the game and click on any gun, you should see your points increased depending on the damage of a weapon.
