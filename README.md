
# **Time to Eat** 
Restaurant finder app using ASP.NET, C#, HTML, and C <br><br>
**FOR** anyone including foodies and travelers <br>
**WHO** is looking for a place to eat, especially during odd hours i.e. after 9pm and before 6am <br>
**TIME TO EAT** is a a website/mobile app <br>
**THAT** searches for restaurants based on business hours or time <br>
**UNLIKE** other restaurant apps such as Yelp that mainly looks at restaurant reviews and finds restaurants based on location, price, and cuisine, <br>
**OUR PRODUCT** is the only app that conveniently lists all open restaurants in one click <br>

# INSTALLATION

# CODING GUIDELINES
## <b>Submitting Code</b>
- Before starting a working session do a pull to be on the latest Changes. Unit Tests must pass before submitting.
- Do a pull before submitting.  If you have a pull conflict, stash your changes, pull, then apply. That way you are working on the latest before submitting 
- 1 Change 1 Submit – Each submit should be about a single change, bug, update 
- Make all submits atomic and small as possible.    
- Use the Stage feature to break up large commits into atomic commits 

- Commit Syntax is what Changed to what file Example:   

    - ```"Added name attribute to ItemModel.cs"```

    - ```"Added name label with localized string to ItemPage.xaml, and ItemPage.xaml.cs"``` 
## <b>Commenting</b>
<i>The code should be readable, so consider the comments as adding context when needed.</i>   


<b>Class</b>   
Use /// to enter a summary comment block 

```cs
/// <summary>   
/// Create page   
/// </summary>   
public class Createmodel : pagemodel 
```
<b>Attribute, Property, Variable </b>   
Use the // comment above the attribute, property, variable 

```cs
// Data middle tier 
public JsonFi1eProductService ProductService { get; } 
```
<b>Important aspect of the code</b>   
Use the // Comment and explain why the code does what it does
```cs
product = productService.CreateData();    

// Redirect the webpage to the update page populated With the data so the user can fill in the fields 
return RedirectToPage(" ./update", new { Id = Product.Id });
``` 
```cs
// Get the current set, and append the new record to it becuase It-numerable does not have Add 
var dataset = GetA11Data(); 
dataset = dataset.Append(data); 
SaveData(dataSet); 
```

 ## <b>Naming</b>
<b>Methods </b>   
Name Methods with Action Verbs. ```GetResult```, rather than ```Results``` etc. 

Have methods return something (not void, bool is OK), makes Unit Test easier to write   
 

<b>Models</b>   

Put Models at the end of Models.  So ```PhoneModel.cs``` instead of ```Phone.cs``` 

```{ get; set; }``` needed if the model round trips to the database or is used in a ```POST``` 

```public string Name { get; set; }```   
instead of 
```public string Name;```

Set default value after the variable in the constructor 

```public string Name { get; set; } = "Unknown";```   

<b>Enums </b>   

Put Enums into their own class under the enum folder  
Use the name "Enum" in the name  
All positions must have values  
Position 0 is always Unknown  

Example: 
 
```cs
public enum PhoneStatusEnum 
{ 
    Unknown = 0,    // unknown 
    Active = 1,     // Active Phone 
    Inactive = 2,   // Disabled Phone 
    Damaged = 3,    // Phone reported as damaged 
    Missing = 4,    // Phone reported as missing 
    Request = 5,    // Request for Provisioning 
    Pending = 6,    // Pending Provisioning 
    Blocked = 7,    // Phone is Blocked 
} 
```

<b>File Names</b>   

Do not use Spaces in File Names, and use lower case (android does not like upper case), use _ to separate words 

``phone_image.png`` instead of <s>``Phone Image.png``</s>   
No special characters in file names <s>``\ / $ % ^``</s> etc., just ``abc`` 

<b>CSS Naming </b>   
Make sure CSS names are consistent across the project 
Have the name describe what it is, not the scenario or where it is used 

Accepted   ```.Title_Black```  
Avoid <s>```.HomePage_Title```</s>

## <b>Coding Standard  </b> 
Brackets under the line 

```cs
// Accepted 
Method (abc) 
{ 
    a=a+1 
} 

// Prohibited 
Method (abc) { 
    a=a+1 
} 
```

If statements must have brackets even if a single line. 
```cs
// Accepted 
if (true) 
{ 
    return; 
}

// Prohibited 
if (true) return; 
```

Fast Fail Pattern 
```cs
// Accepted 
if (test==false) 
{ 
    return false; 
} 
a=a+1 
return true; 

// Prohibited
if (test != false) 
{ 
    a=a+1 
    return true; 
} 
return false; 
```

Avoid !  Negation 
```cs
// Accepted 
if (false) 
{ 
    return; 
} 

// Avoid 
if (!true) 
{ 
    return; 
} 
```

Avoid Else statements 

```cs
// Accepted 
if (a) 
{  
    b = b + 1; 
} 
else 
{ 
    b = b + 2; 
} 
return b; 

// Avoid 
if (a) 
{ 
    return b+1; 
} 
return b+2; 
```
 
Refactor methods to do one thing, and call sub methods to simplify code 

```cs
// Accepted 
Bool ValidateData() 
{ 
    if (CheckOutOfBounds(a))
    { 
        return false; 
    } 

    if (ValueInvalid(a))
    { 
        return false; 
    } 
    a = a * 100; 
    return true; 
}

// Avoid 
if (A < 0 && A > 100) 
{ 
    if (a == 5 || a == 6) 
    { 
        a = a * 100 
    } 
    return true; 
} 
```
Every Method must have a /// Code block     
Every variable must have a // comment above it   
White space is your friend, separate blocks with white space    

```cs
// Accepted 
var a=1; 
var b = 2; 
a = a + b; 
if (a > 10) 
{ 
    a = 10; 
} 
return a; 

// Avoid 
var a = 1; 
var b = 2; 
a = a + b; 
if (a > 10) 
{ 
    a = 10; 
} 
return a; 
```

One line per variable 
```cs
// Accepted 
int a; 
int b; 
// Avoid 
int a,b; 
```

Method Names should be action based 

```cs
// Accepted 
bool ValidateName() {}

// Avoid 
bool NameTooLong() {}
bool GetName() {}
bool RecordName() {} 
bool ConvertNameToRecordID
Bool RecordID() 
```
 
Method Names are CaseSensitiveNames 

```cs
// Accepted 
CaseSensitiveNames 

// Avoid 
lowerCaseFirstLetterNames 
UpperCaseAllFirstLetters 
Uppercasefirstletteronly 
```

 

Models should have Model in the name 

```cs
// Accepted 
ProductModel 

// Avoid 
Product 
```


Enums, must have values, and 0 stands for undefined 

```cs
// Accepted 
Enum state 
{ 
Unknown = 0, 
Valid = 1, 
InValid = 10, 
Rejected = 20 
} 

// Avoid 
Enum state 
{ 
Valid, 
InValid 
Rejected 
} 
```

Avoid Compound If Statements 

Or ||, and && make unit testing very difficult, better to have independent If statements 

```cs
// Accepted 
If (A < 0) 
{ 
     return null 
} 

If (A > 100) 
{ 
    return false; 
} 

// Avoid 
if (A <0 || A > 100) 
{ 
    return false; 
} 

```

## <b>Unit Test</b>
Test File name shall be the class file name + ```"Tests"```
``FileService`` class to ``FileServiceTests`` 

``Test`` file shall be in the same folder structure as ``Class`` file 

Use the region tag to group all the tests for a method together in the file 

```cs
#region MethodName 

#endregion  MethodName 
```

Method name Format 

``Method_Condition_State_Reason_Expected ``

Example: 

```DialInOnBoxes_Valid_Test_Reading_1_Should_Return_IsBadImage_False``` 

```DialInOnBoxes_InValid_Test_Invalid_13_All_White_Should_Return_IsBadImage_True``` 

 

Use the template of // for each test  
```cs
// Arrange 
// Act 
// Reset 
// Assert 
```

Variable Naming 

```cs
// Arrange 
{
// if you have data you setup, call it data 
var data = "xxxxx" 
} 
 
// Act 
( 
var result =   something(data) 
note result, not results 
) 
```
 

Assert 

Each UT should test 1 condition, usually only 1 assert is needed to validate.  OK to add more validation if that helps lock in the functionality ( common for validating data models) 

 

use Assert.AreEqual, I am trying to standardize on that so all the UTs look the same.  Makes it easier for people to maintain over time. 

```Assert.AreEqual(false, result.IsBadImage); ```