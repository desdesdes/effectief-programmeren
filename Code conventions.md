# Coding conventions

# General

## General

The Development Standards consist of rules and recommendations regarding specific development topics. Every developer should be familiar with and adhere to these standards when developing. The standard are based on [Microsoft coding conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions) and [Microsoft Framework design guidelines](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/). 

#### G001: Rules must be followed at all times

Exceptions to rules are not allowed. Either a request can be made to change the rule, or the code should be changed so it conforms to the rule. 

Some rules are validated during static code analysis in Visual Studio. When the code does not adhere to these rules, the code analysis will result in errors. In addition, many technical rules not documented in the Development Standards are checked during static code analysis.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### G002: Every time a recommendation is not followed, this must have a good reason and should be documented

Exceptions to recommendations are only allowed in exceptional cases. If necessary, a request can be made to change a recommendation. Exceptions to recommendations should be discussed during a code review. Good reasons do not include personal preferences of style. When a recommendation is not followed, this should be documented in the code or in the suppression of the code analysis warning.

Some recommendations are validated during static code analysis in Visual Studio. When the code does not adhere to these recommendations, the code analysis will result in a warning. In addition, many technical rules not documented in the Development Standards are checked during static code analysis. In exceptional cases, these warnings can be suppressed. Suppressions should be made as close to the problem as possible and should include a justification. If you need a global suppression create or use a *GlobalSuppressions.cs* file in the root level of the project, but use it only when there is no other option.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### G003: Prefix rule and recommendation numbers with the prefix corresponding with the topic and never change the number

Numbers should never be changed, because this would invalidate all references to the rule or recommendation. There's a prefix for every topic.

Standards regarding a specific development topic should be documented under the corresponding topic. If a rule regards several topics, the rule should be added to the topic that's most specific to the rule. For example, when a rule describes the naming of Exceptions, the rule should be added to the *Naming Standards*, because all naming standards are included in the *Naming Standards*. However, when a rule regards the specific design of exceptions, it should be added to the *Exceptions Rules*, because the *Design Rules* only include general design issues.

|**Topic**|**Prefix**|**Description**|
| :- | :- | :- |
|General|G|Standards that do not fall in other categories|
|Naming|N|Everything regarding to naming|
|Design|D|General use of object orientation and general code design|
|Maintainability|M|Code constructs and syntax|
|Performance|P|Performance|
|UI|U|UI - UX|

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

# Naming

## General

#### N001: Use US-English as the Naming Language

Use US-English for naming identifiers wherever possible. 

Dutch should be used for dutch only business terms. Prefixes and postfixes to the identifier should be in English.
Examples:
- _Inkoop order_ in Dutch is _Purchase order_ in English. The identifier should be named _PurchaseOrder_, not _InkoopOrder_, because en english the term also makes sense.
- _Kleine Ondernemers Regeling_ is an arrangement for small businesses in the Netherlands. The identifier should be named _KleineOndernemersRegeling_, because translated term _SmallBusinessRegulation_ does not make sense in English, because the regulation does not exist there.
- A function to get the tabs for _KleineOndernemersRegeling_ should be named _GetKleineOndernemersRegelingTabs_ and not _VerkrijgKleineOndernemersRegelingTabbladen_. This is because english and dutch terms can be mixed and theis would result in a mix of prefixes and postfixes in code which makes it very hard to located functions.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft|Only uses english|
|Focus||
|Profit||

#### N002: Name an identifier according to its meaning and not its type

Avoid using language specific terminology in names of identifiers. As an example, suppose you have a number of overloaded methods to write data types into a stream:

```csharp
void Write(double doubleValue); 	//Wrong
void Write(long longValue); 		//Wrong
void Write(double value); 		    //ok
void Write(long value); 		    //ok
```

```vbnet
Sub Write(doubleValue as Double); 	'Wrong
Sub Write(longValue as Long); 		'Wrong
Sub Write(value as Double); 		'ok
Sub Write(value as Long); 		    'ok
```

If it is absolutely required to have a uniquely named method for every data type, use Universal Type Names in the method names. The table below provides the mapping from C# types to Universal types.

|**C# Type Name**|**VB Type Name**|**Universal Type Name**|
| :- | :- | :- |
|bool|Boolean|Boolean|
|byte|Byte|Byte|
|sbyte|SByte|SByte|
|char|Char|Char|
|decimal|Decimal|Decimal|
|double|Double|Double|
|float|Single|Single|
|int|Integer|Int32|
|uint|UInteger|UInt32|
|long|Long|Int64|
|ulong|ULong|UInt64|
|object|Object|Object|
|short|Short|Int16|
|ushort|UShort|UInt16|
|string|String|String|

Based on the example above, the corresponding reading methods may look like this:

```csharp
double ReadDouble() { };
long ReadInt64() { };
```

```vbnet
Function ReadDouble() As Double;
Function ReadInt64() As Long;
```

**Note:** the type names used in the Read methods are the universal type names!!!

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft|Same [see](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/general-naming-conventions)|
|Focus||
|Profit||

#### N003: Do NOT use Hungarian notation

Do not use Hungarian notation or add any other type identification to identifiers. The use of Hungarian notation is deprecated by companies like Microsoft because it introduces a programming language-dependency and complicates maintenance activities. Only use the naming additions as described in this document.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft|Same [see](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/general-naming-conventions)|
|Focus||
|Profit||

#### N004: Do NOT use underscores

Underscores are not allowed in names.

Exceptions:
- Private fields *must* start with an underscore prefix
- Eventhandlers, e.g. okButton\_OnClick, ConfigProcessing\_Completed

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft|Same [see](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/general-naming-conventions)|
|Focus||
|Profit||

#### N005: Do NOT use a default prefix or suffix for identifiers

Do not prefix or suffix classes with *Class*, methods with *Method*, structs with *Struct*, enums with *Enum*, delegates with *Delegate* etc.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft|Same [see](hhttps://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-classes-structs-and-interfaces)|
|Focus||
|Profit||

#### N006: Name the source file to the main class

In addition, do not put more than one top level class in one sourcefile.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### N007: Use inquiring names for boolean fields, variables, properties and parameters

The name of boolean fields, variables, properties and parameters should always be composed of a verb (Is, Has, Does, Can, etc.) and an adjective or noun (Enabled, Visible, Dirty, UpdateNeeded, Children, etc.), for example: IsEnabled, IsVisible, IsDirty, IsUpdateNeeded, HasChildren. It should be possible to use the name as a question with two possible answers: yes or no. If it is (logically) possible to start the name with 'Is', then 'Is' should be used.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### N008: Do NOT use negations

Never use (implicit) negations when naming variables, functions, properties, etc.

|**Wrong**|**OK**|
| :- | :- |
|IsNotFilled|IsFilled|
|IsNotInitialized|IsInitialized|
|IsDisabled|IsEnabled|
|IsInvisible|IsVisible|
|IsHidden|IsDisplayed|

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### N009: Prefer the word not to use word *object* in documentation when referencing a type or instance.

The word object is a very generic word and should be avoided in documentation. It is often used to descript a type, but also just as often used to describe an instance of the type. Prefer to use the word *instance* or *type*.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

## Classes and Structs

#### N010: Use a noun for naming classes

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

### Enums

#### N011: Use an enum to strongly type parameters, properties, and return types

This enhances clarity and type-safety. Try to avoid casting between enumerated types and integral types.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### N012: Use singular names for enumeration types representing a choice

For example, do not name an enumeration type Protocols but name it Protocol instead. Consider the following example in which only one option is allowed.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### N013: Use a plural name for enumerations representing bitfields

The following code snippet is a good example of an enumeration that allows combining multiple options.

```csharp
[Flags]
public enum SearchOptions
{
    CaseInsensitive = 0x01,
    WholeWordOnly = 0x02,
    AllDocuments = 0x04,
    Backwards = 0x08,
    AllowWildcards = 0x10
}
```

```vbnet
<Flags>
Public Enum SearchOptions
    CaseInsensitive = &H01
    WholeWordOnly = &H02
    AllDocuments = &H04
    Backwards = &H08
    AllowWildcards = &H10
End Enum
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### N014: Do not prefix of postfix name a (abstract) (MustInherit in VB) base classes with *Base*

There should always be an option to add a base class to a class. If the base class is named *Base*, that name would be BaseBase? Also the word base does not make it cleaner. It is better to use a name that describes what type represents.

SqlConnection is a good example of a class that represents a connection to a SQL database. It is not named SqlConnectionBase.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommandation|
|Microsoft||
|Focus||
|Profit||

### Interfaces

#### N015: Interfaces should be prefixed with the letter I

All interfaces should be prefixed with the letter *I* ('I' capital). Use a noun (e.g. *IComponent*), noun phrase (e.g. *ICustomAttributeProvider*), or an adjective (e.g. *IPersistable*) to name an interface.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

### Methods

#### N016: A method name should correspond a methods behavior

Use names that truly correspond to the functional behavior of a method. It should not be named after the internal implementation or after the caller or callee. If a method does not yield the expected result or produce the expected behavior, it should raise an exception.

Example: If a method is called GetValue(), it should always return the value. But if the value cannot be found, retrieved, etc. it should throw an exception because it cannot return the value. If the method should return null if the value cannot be retrieved, use a name that implies this, e.g. GetValueOrDefault().

Try: If a method can fail because of business logic (eg.it is expected to fail), consider adding an equivalent method that allows to check for success without the exception being thrown. Name this method TryXxx(), returning true for success/false for failure when possible.

Example:

```csharp
// Get the value that corresponds to the key
// Throw an exception if the key does not exist
private string GetValue(string key)
{
}

// Get the value that corresponds to the key
// Returns null if the key does not exist
private string GetValueOrDefault(string key)
{
}

// Get the value that corresponds to the key
// Returns true if the key was found, false if not
private bool TryGetValue(string key, out string value)
{
}

// Delete the specified file
// Throw an exception if the file cannot be deleted
private void DeleteFile(string filePath)
{
}

// Delete the specified file
// Returns true if the file was deleted, of false if it was not
private bool TryDeleteFile(string filePath)
{
}
```

```vbnet
' Get the value that corresponds to the key
' Throw an exception if the key does not exist
Private Function GetValue(key as String) As String
End Function

' Get the value that corresponds to the key
' Returns null if the key does not exist
Private Function GetValueOrDefault(key as String) As String
End Function

' Get the value that corresponds to the key
' Returns true if the key was found, false if not
Private Function TryGetValue(key As String, <Out> ByRef value As String) As Boolean
End Function

' Delete the specified file
' Throw an exception if the file cannot be deleted
Private Sub DeleteFile(filePath As String)
End Sub

' Delete the specified file
' Returns true if the file was deleted, of false if it was not
Private Function TryDeleteFile(filePath As String) As Boolean
End Function
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### N017: Use a Verb followed by a Noun for naming Methods

Use a verb first followed by a noun, example: GetEmployeeName, UpdateEmployee, IsValidUserName. Note that properties do not have to comply to this rule.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### N018: Do NOT add 'Callback' or a similar suffix to callback Methods

Do not add suffixes like *Callback* or *CB* to indicate that methods are going to be called through a callback delegate. You cannot make assumptions on whether methods will be called through a delegate or not. An end-user may decide to use Asynchronous Delegate Invocation to execute the method.

```csharp
static void DemoCallback(System.IAsyncResult ar) { } // Wrong
static void DemoCB(System.IAsyncResult ar) { }       // Wrong
```

```vbnet
Sub DemoCallback(System.IAsyncResult ar) ' Wrong
End Sub
Sub void DemoCB(System.IAsyncResult ar) ' Wrong
End Sub
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

### Collections

#### N019: Use a plural name for variables that represent an array or collection type

Use plural names for arrays and collection types to make clear it's an array or collection.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

## Casing

#### N020: Use the proper casing for all identifiers

|**Identifier**|**Case**|**Example**|**Note**|
| :- | :- | :- | :- |
|Namespace|Pascal|System.Drawing||
|Class|Pascal|AppDomain||
|Struct|Pascal|Record||
|Delegate|Pascal|Calculation||
|Interface|Pascal|IDisposable|Use ‘I’, followed by a capital|
|Enum value|Pascal|FatalError||
|Event|Pascal|ValueChanged||
|Event handler delegate (if not using EventHandler<>)|Pascal|ValueChanged**EventHandler**|Use eventname + 'EventHandler'|
|Event handler|Pascal|Item\_ValueChanged|Use a field- or descriptive name + ‘\_’ + eventname|
|Field/Constant (private)|Camel|\_name|Exception: UI designer types do not have to start with a underscore.|
|Field/Constant (not private)|Pascal|Name||
|Method|Pascal|ToString||
|Property|Pascal|Name||
|Parameter/argument|Camel|typeName||
|Variable|Camel|color||
|Generic type|Pascal|TKey|Start with T|

Tip:
Use a .editorconfig file to enforce the casing rules. This file can be added to the root of the solution and will be used by Visual Studio to enforce the rules.

Remarks:
Microsot has multiple competing standards for static field, one uses _ (same as instance fields), one use s_ and one uses s_ and t_
- https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names
- https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-type-members#names-of-fields

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft|Same [see](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names)|
|Focus||
|Profit||

#### N021: Use the correct casing for Abbreviations

Two-letter abbreviations in Pascal casing have both letters capitalized. In Camel casing this also holds true, except at the start of an identifier where both letters are written in lower case. With respect to capitalization in Pascal and Camel casing, abbreviations with more than two letters are treated as ordinary words. Some examples:

|**Camel Casing**|**Pascal Casing**|
| :- | :- |
|uiEntry|UIEntry|
|cmsEntry|CmsEntry|
|demoUI|DemoUI|

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

# Design
## General
### Constructors and destructors

#### D001: Do not create a constructor that does not yield a fully initialized instance

Only create constructors that construct instances that are fully initialized. There should be no need to set additional properties.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### D002: Do not directly or indirectly call virtual or abstract methods (Overridable / MustOverride in VB) in a constructor

This causes strange constructor behavior when the virtual method is called because the virtual method on the deriving type is executed before the constructors are fully executed.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### D003: Constructors should be fast 

Constructors should be fast. If a constructor is slow, consider using a static method (Shared in VB) returning the instance instead.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### D004: Limit the amount of exceptions thrown from constructor

Constructors shouyld only throw a limited amount of exceptions. Never throw exceptions from static constructors (Shared in VB), because developers do not expect that behavior and have no control over when the static constructor is called (Shared in VB).

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

### Fields

#### D005: Never declare fields external

Fields should never be declared external.

Exceptions:
- Static readonly fields and constants, which may have any accessibility deemed appropriate.

Non-private fields do not use an underscore prefix, instead name it as a property (Pascal cased).

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### D006: Static methods should be thread safe (shared / module in VB), instance methods *can* be thread safe.  

Document if a class instance *can* be used across threads, a static function that cannot be used across threads should not exist. 

This basically means that you should not use static fields (shared fields or module fields in VB) in a static method, unless the field is readonly and immutable.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### D007: Do not build write-only properties

Always implement a property getter or use a method instead.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

### Methods

#### D008: Make functions static (shared in VB) if they are stateless and non-exposed

A non-exposed method that does not use any instance fields should be static (shared in VB). An exposed method that does not use any instance fields should be static (shared in VB) if it is appropriate. This means that is should be made static is you do not expect it to use instance fields in the future.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### D009: If you have a public method and a protected which the same name postfix the protected method with *Core* or *Internal*

This makes it easy for developers to recognise. The public method should call the protected method and both methods should have the same purpose and have similar behavior, if not different names should be used.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

## .NET

### Constructors and destructors

#### D010: Destructors should be fast (Finilizer in VB)

Destructors (Finilizer in VB) should be fast, because there is only one thread per process that calls the finalizer. If the destroctor is slow it might take a long time for other destructors to be called limiting .NET ability to release resources. 

Consider using the IDisposable pattern instead of a destructor.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

### Types

#### D011: Do not share type instance between threads unless it is documented that they are thread safe.

You should not assume a instance of a type is thread safe unless it is documented that it is.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### D012: Put external types on the namespace level instead of inside another type

Arrange the external types on the namespace level, not on class level.

Exceptions: private classes, structs, enums and delegates must be nested inside another type. When using private classes or structs, consider creating a partial class and use a seperate code file for each substantial private class or struct.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

### Classes and structs

#### D013: When to use a struct instead of a class

A *struct* should be considered for types that meet any of the following criteria:
- Value type semantics are desired.
- Act like primitive types.
- Have an instance size under +/- 16 bytes.
- Are immutable.

Remember that a *struct* cannot be derived from, and cannot contain a user defined parameterless constructor.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### D014: Extension methods are only allowed when they are appropriate and meaningful throughout their namespace

Since extension methods are visible throughout the namespace they are defined in, they should have the same meaning and relevant usage throughout that namespace. 

So if an extension method is only relevant for a specific class, it should be defined in the same namespace as that class. If it is relevant for a specific set of classes, it should be defined in the same namespace as those classes. If it is relevant for a specific set of classes in a specific context, it should be defined in the same namespace as those classes in that context.

This will make it easy to find a specific extension method using intellisense, but will also prevent intellisense from being cluttered with irrelevant extension methods.

Consider you have a POCO type Employee living in Afas.MyApp.Hrm.Employee. We have a extention method ToJson that converts the type to a JSON string.

We are using the Employee type in Afas.MyApp.Crm.Person. If we only need the ToJson method in the Person context, we should put it in Afas.MyApp.Crm.Person. If we only need it in the Crm context, we should put it in Afas.MyApp.Crm. But if we need it everywhere where the Employee type is used, we should put it in Afas.MyApp.Hrm.Employee. We could even consider adding it to the System.Text.Json namepace so it is only visible when we add that namespace.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

### Events and event handlers

#### D015: Use a verb for naming an event

Good examples of events are Closing, Minimized, and Click. 

For example, the declaration for the Closing event may look like this:

public event EventHandler Closing;

```csharp
public event EventHandler Closing;
```

```vbnet
Public Event Closing As EventHandler
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### D016: Do not add an Event suffix to the name of an event

Do not add an *Event* suffix or any other type-related suffix to the name of an event.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### D017: Use an -ing and -ed form to express pre-events and postevents

Use an *-ing* and *-ed* form to express pre-events and postevents (*Closing* / *Closed*). Do not use a pattern like *BeginXxx* and *EndXxx*. If you want to provide distinct events for expressing a point of time before and a point of time after a certain occurrence such as a validation event, do not use a pattern like *BeforeValidation* and *AfterValidation*. Instead, use a *Validating* and *Validated* pattern.

Typically postevens do not have an option to cancel the action, because it has already executed. Pre-event do tend to have have this option.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### D018: Prefix event raising methods with 'On'

If a class can be inherited from (i.e. not sealed), use a virtual method to raise the event in. That way, the derived class is able to override the behaviour and even prevent the event from being raised. This method should be called OnEventName, in which EventName always is the name of the event being raised.

```csharp
public event EventHandler Completed;

public virtual void OnCompleted(EventArgs e)
{
    if (Completed != null)
    {
        Completed(this, e);
    }
}
```

```vbnet
Public Event Completed As EventHandler

Public Overridable Sub OnCompleted(e as EventArgs)
    RaiseEvent Completed(Me, e)
End Sub
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### D019: Use Name_EventName for event handling method

Event handling methods can either handle an event of a specific field, or can be a generalized handler. When handling an event of a specific field, use the name of that field (without '_' prefix) as the Name in Name\_EventName. If the method is a generalized handler, the Name in Name_EventName should describe the source of the event.

```csharp
// Event handler for an event of a specific variable
private void okButton_Click(EventArgs e)
{
}

// Generalized event handler
private void Calculation_Completed(EventArgs e)
{
}
```

```vbnet
' Event handler for an event of a specific variable
Private Sub okButton_Click(e As EventArgs)
End Sub

' Generalized event handler
Private Sub Calculation_Completed(e As EventArgs)
End Sub
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### D020: Raise events through a protected virtual method (Protected Overridable in VB) if class is not static or sealed (Module or NotInheritable in VB)

If a derived class wants to intercept the throwing of an event, it can override such a virtual method, do its own work, and then decide whether or not to call the base class version. Since the derived class may decide not to call the base class method, ensure that it does not do any work required for the base class to function properly.

Name this method *OnEventName*, where EventName should be replaced with the name of the event. Notice that an event handler uses the same naming scheme but has a different signature. The following snippet (most parts left out for brevity) illustrates the difference between the two.

```csharp
// An example class
public class Connection
{
  // Event definition
  public event EventHandler Closed;

  // Method that causes the event to occur
  public void Close()
  {
    // Do something and then raise the event
    OnClosed(EventArgs.Empty);
  }

  // Method that raises the Closed event.
  protected virtual void OnClosed(EventArgs args)
  {
    if (Closed != null)
    {
        Closed(this, args);
    }
  }

  // Main entrypoint
  public static void Main()
  {
    Connection connection = new Connection();
    connection.Closed += Connection_Closed;
  }

  // Event handler for the Closed event.
  private static void Connection_Closed(object sender, EventArgs args)
  {
  }
}
```

```vbnet
public class Connection
    'Event definition
    Public Event Closed As EventHandler

    ' Method that causes the event to occur
    Public Sub Close()
        ' Do something and then raise the event
        RaiseEvent Closed(Me, EventArgs.Empty)
    End Sub

    ' Method that raises the Closed event.
    Public Sub OnClosed(e As EventArgs)
        RaiseEvent Closed(Me, e)
    End Sub

    ' Main entrypoint
    Public Shared Sub Main()
        Dim connection As New Connection()
        AddHandler connection.Closed, AddressOf Connection_Closed
    End Sub

    Private Shared Sub Connection_Closed(sender As Object, e As EventArgs)
    End Sub
End Class
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### D021: Use the sender/arguments signature for event handler delegates

All event handler delegates must have the following signature:

```csharp
void Xxx(object sender, EventArgs arguments);
```

```vbnet
Sub Xxx(sender As Object, arguments As EventArgs)
```

Using the base class as the sender type allows derived classes to reuse the same event handler. The same applies to the arguments parameter. It is recommended to derive from the .NET Framework’ s *EventArgs* class and add your own event data. Using such a class prevents cluttering the event handler’ s signature, allows extending the event data without breaking any existing users, and can accommodate multiple values. Moreover, all event data should be exposed through properties, because that allows for verification and preventing access to data that is not always valid in all occurrences of a certain event. Consider using the EventHandler<> generic delegate for the event signature. Example:

```csharp
public class MyEventArgs : EventArgs //Derive from EventArgs
{
    public MyEventArgs(string extraInfo)
    {
        ExtraInfo = extraInfo;
    }

    public string ExtraInfo { get; }
}
```

```vbnet
Public Class MyEventArgs
  Inherits EventArgs 'Derive from EventArgs

  Public Sub New(extraInfo as String)
    _extraInfo = extraInfo
  End Sub

  Public ReadOnly Property ExtraInfo As String
End Class
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

### Delegates types

#### D022: Add Callback to delegate types related to callback methods

Delegate types that are used to pass a reference to a callback method (so not an event) must be suffixed with Callback. For example:

```csharp
public delegate void AsyncIOFinishedCallback(IpcClient client, string message);
```

```vbnet
Public Delegate Sub AsyncIOFinishedCallback(client As IpcClient, message As String)
```

Do not add *Callback* to callback methods!

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft|Same [see](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-classes-structs-and-interfaces)|
|Focus||
|Profit||

#### D023: Use Action, Func and EventHandler generics when possible

Using generic delegates allows more efficient memory usage and prevents cluttering the types which lots of delegate types.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

### Enums

#### D024: Use an enum to strongly type parameters, properties, and return types

This enhances clarity and type-safety. Try to avoid casting between enumerated types and integral types.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### D025: Use the [Flags] (<Flags> in VB) attribute on an enum if a bitwise operation is to be performed on the numeric values

Use an *enum* with the *flags* attribute only if the value can be completely expressed as a set of bit flags. Use a plural name for such an *enum*. Do not use an *enum* for open sets (such as the operating system version). 

Do not use sentinel values, to indicate e.g. first or last value. All members of the enum must have a meaning that applies to the enum.

```csharp
// The protocol is always one of these
public enum Protocol
{
  Tcp,
  Udp,
  Http,
  Ftp,
  // Do not use this:
  ProtocolCount
}
```

```vbnet
' The protocol is always one of these
Public Enum Protocol
  Tcp
  Udp
  Http
  Ftp
  ' Do not use this:
  ProtocolCount
End Enum
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### D026: Use delegate inference instead of explicit delegate instantiation

```csharp
delegate void SomeDelegate();

public void SomeMethod()
{
}

public void SomeMethod2()
{
  SomeDelegate del = SomeMethod; // OK
  SomeDelegate del = new SomeDelegate(SomeMethod); // Wrong
}
```

```vbnet
Delegate Sub SomeDelegate()

Public Sub SomeMethod()
End Sub

Public Sub SomeMethod2()
  Dim del as SomeDelegate = AddressOf SomeMethod ' OK
  Dim del as new SomeDelegate(AddressOf SomeMethod) ' Wrong
End Sub
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### D027: Use lambda expression over anonymous delegated

VB does not support anonymous delegates.

```csharp
private Action _action;

public void Demo()
{
    _action = () => { Console.WriteLine("aa"); }; // OK
    _action = delegate { Console.WriteLine("aa"); }; // Wrong
}
```

```vbnet
Private _action As Action

Public Sub Demo()
    _action = Sub() Console.WriteLine("aa") ' OK
End Sub
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

### Interfaces

#### D028: Consider attributes over empty interfaces

Empty interfaces are not recommended. An interface is designed to be a contract, but an empty contract has no meaning. Consider using an attribute instead.

An empty interface can be used to group a set of base classes or interfaces, but you should consider building overloads for the specific types instead of having the function accept the interface. This provides cleaner code and better performance.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### D029: Use explicit interface implementation only to prevent name-clashing or to support optional interfaces

When you use explicit interface implementation, then the methods implemented by the class involved will not be visible through the class interface. To access those methods, you must first cast the instance to the requested interface. It is not recommended to use explicit interface implementation.

Exceptions:
- When you want to prevent name clashing. This can happen when multiple interfaces must be supported which have equally named methods, or when an existing class must support a new interface in which the interface has a member which name clashes with a member of the class.
- When you want to support several optional interfaces (e.g*. IEnumerator, IComparer*, etc) and you do not want to clutter your class interface with their members.

Consider the following example:

```csharp
public interface IFoo1
{
  void Foo();
}

public interface IFoo2
{
  void Foo();
}

public class FooClass : IFoo1, IFoo2
{
  // This Foo is only accessible by explictly casting to IFoo1
  void IFoo1.Foo() {  }

  // This Foo is only accessible by explictly casting to IFoo2
  void IFoo2.Foo() {  }
}
```

```vbnet
Public Interface IFoo
  Sub Foo()
End Interface

Public Interface IFoo2
  Sub Foo()
End Interface

Public Class FooClass
  Implements IFoo, IFoo2

  ' This Foo is only accessible by explictly casting to IFoo1
  Private Sub IFoo_Foo() Implements IFoo.Foo
  End Sub

  ' This Foo is only accessible by explictly casting to IFoo2
  Private Sub IFoo2_Foo() Implements IFoo2.Foo
  End Sub
End Class
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### D030: Suffix attribute classes with Attribute

Classes that represent an attribute should always have an Attribute suffix. When using an attribute, omit the suffix:

```csharp
public class FormattingAttribute : Attribute
{
}

[FormattingAttribute("###-##-####")] // Wrong, suffix is not omitted
public int socialSecurityNumber;

[Formatting("###-##-####")]          // OK, suffix is omitted
public int socialSecurityNumber;
```

```vbnet
Public Class FormattingAttribute
  Inherits Attribute
End Class

<FormattingAttribute("###-##-####")> ' Wrong, suffix is not omitted
Public socialSecurityNumber As Integer

<Formatting("###-##-####")>          ' OK, suffix is omitted
Public socialSecurityNumber As Integer
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft|Same [see](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-classes-structs-and-interfaces)|
|Focus||
|Profit||

#### D031: Suffix exception classes with Exception

Suffix exception classes with *Exception*.

For example: FileNotFoundException.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft|Same [see](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-classes-structs-and-interfaces)|
|Focus||
|Profit||

#### D032: Suffix Collections with the appropriate suffix

Suffix the name of types implementing IEnumberable with the name the behavior they implement:

Key/Value like behavior à …Dictionary

Stack like behavior à …Stack

Queue like behavior à …Queue

Set like behavior (can only contain items once) à …Set

List like behavior (indexer property) à …List

All other à …Collection

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft|Different, only uses dictionary and collection [see](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-classes-structs-and-interfaces)|
|Focus||
|Profit||

#### D033: Suffix static helper classes with "Utility" or "Extensions"

We distinguish two types of helper classes:

- Static classes that contain general members for a specific type or problem domain, these classes should be named "<name>Utility".
- Static classes that contain extension methods for a specific type or problem domain, these classes should be named "<name>Extensions".

A 'Utility' class should not contain any extensions methods, and an 'Extensions' class should not contain any public other non-extension helper methods.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

### Constructors and destructors

#### D034: Do not access any reference type members in the destructor

When the destructor is called by the GC, it is very possible that some or all of the instances referenced by class members are already garbage collected, so dereferencing those instances may cause exceptions to be thrown. Only value type members can be accessed (since they live on the stack)

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### D035: When implementing a Destructor always implement the IDisposable pattern

If a destructor is required, adhere to: [Rule D030: Implement IDisposable if a class uses unmanaged or expensive resources or contains other IDisposables](#rule-d030-implement-idisposable-if-a-class-uses-unmanaged-or-expensive-resources-or-contains-other-idisposables)

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### D036: Implement IDisposable if a class uses unmanaged or expensive resources or contains other IDisposables

If a type uses unmanaged resources such as handles returned by C/C++ DLLs or IDisposable resources, you must implement the *IDisposable* interface to allow type user to explicitly release such resources. Always implement the destructor that calls Dispose(false), so it is possible to detect when Dispose() was not called. Since Dispose() (which calls Dispose(true)) must always be called on an IDisposable instance, Dispose(false) should never occur. Since the destructor is suppressed in the Dispose(), it will be called only if the instance was not disposed explicitly. To help ensure that resources are always cleaned up appropriately, a Dispose method should be callable multiple times without throwing an exception.

The follow code snippet shows the pattern to use for such scenarios:

```csharp
public class ResourceHolder : IDisposable
{
  // Implementation of the IDisposable interface
  public void Dispose()
  {
    // Call internal Dispose(bool)
    Dispose(true);
    // Prevent the destructor from being called
    GC.SuppressFinalize(this);
  }

  // Central method for cleaning up resources
  protected virtual void Dispose(bool disposing)
  {
    // If disposing is true (which should always be the case), 
    // then this method was called through the public Dispose()
    if(disposing)
    {
      // Release or cleanup managed resources, such as IDisposable instances
    }

    // Always release or cleanup (any) unmanaged resources
  }

  // Always add the destructor (not in derived classes, optional in a sealed class)
  ~ResourceHolder()
  {
    Dispose(false);
  }
}
```

```vbnet
Public Class ResourceHolder
  Implements IDisposable ' Implementation of the IDisposable interface

  Public Sub Dispose() Implements IDisposable.Dispose
    ' Call internal Dispose(bool)
    Dispose(True)
    ' Prevent the destructor from being called
    GC.SuppressFinalize(Me)
  End Sub

  ' Central method for cleaning up resources
  Protected Overridable Sub Dispose(disposing As Boolean)
    ' If disposing is true (which should always be the case),
    ' then this method was called through the public Dispose()
    If disposing Then
      '  Release or cleanup managed resources, such as IDisposable instances
    End If

    'Always release or cleanup (any) unmanaged resources
  End Sub

  ' Always add the destructor (not in derived classes, optional in a NotInheritable class)
  Protected Overrides Sub Finalize()
    ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
    Dispose(False)
  End Sub
End Class
```

If another class derives from this class, then this class should only override the *Dispose(bool)* method of the base class. It should not implement *IDisposable* itself, nor provide a destructor. The base class’ s ‘destructor’ is automatically called.

```csharp
public class DerivedResourceHolder : ResourceHolder
{
  protected override void Dispose(bool disposing)
  {
    if(disposing)
    {
      // Release or cleanup managed resources, such as IDisposable instances
    }

    // Always release or cleanup (any) unmanaged resources
    // Call Dispose on our base class.
    base.Dispose(disposing);
  }
}
```

```vbnet
Public Class DerivedResourceHolder
  Inherits ResourceHolder

  Protected Overrides Sub Dispose(disposing As Boolean)
    If disposing Then
      ' Release or cleanup managed resources, such as IDisposable instances
    End If

    ' Always release or cleanup (any) unmanaged resources
    ' Call Dispose on our base class.
    MyBase.Dispose(disposing)
  End Sub
End Class
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### D037: If you create an instance implementing IDisposable always call Dispose

You do not have to call Dispose on the instance if you use it in a using block.

**Make sure Dispose is always called, from either:**

- a using block
- a finally block
- the Dispose implementation of the class itself (for private fields).

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### D038: Use the decimal type to represent numbers containing decimals

Most floating point values have no exact binary representation and have a limited precision. Consider the following example in which two floats and two decimals are compared:

```csharp
// double is a floating point format
double fa = 3.65, fb = 0.05, fc = 3.7;

// decimal is no floating point format
decimal da = 3.65M, db = 0.05M, dc = 3.7M;

fa += fb;
da += db;

Console.WriteLine(fa == fc); // False
Console.WriteLine(da == dc); // True
```

```vbnet
' double is a floating point format
Dim fa = 3.65, fb = 0.05, fc = 3.7

'decimal is no floating point format
Dim da = 3.65d, db = 0.05d, dc = 3.7d

fa += fb
da += db

Console.WriteLine(fa = fc) ' False
Console.WriteLine(da = dc) ' True
```

Exceptions: 
- Use a double when precision of a decimal is insufficient.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

### Constants

#### D039: Do not use literal values other than 0, 1, null (Nothing in VB) to define constants

Use the following pattern to define constants:

```csharp
public class Whatever
{
  private static readonly Color _papayaWhip = new Color(0xFFEFD5);
  private const int _daysInWeek = 7;
}
```

```vbnet
Public Class Whatever
    Private Shared ReadOnly _papayaWhip As New Color(&HFFEFD5)
    Private Const _daysInWeek = 7
End Class
```

There are exceptions: the values *0,* *1* and *null* (Nothing in VB) can nearly always be used safely. Very often the values 2 and -1 are OK as well. Strings intended for logging or tracing are exempt from this rule. Literals are allowed within a single method when they only apply to that method and their meaning is clear in the context.

mean = (a + b) / 2; //okay

If the value of one constant depends on the value of another, do attempt to make this explicit in the code, so do not write:

```csharp
public class SomeSpecialContainer
{
  private const int _maxItems = 32;
  private const int _highWaterMark = 24; //at 75%, Wrong
}
```

```vbnet
Public Class SomeSpecialContainer
    Private Const _maxItems = 32
    Private Const _highWaterMark = 24 'at 75%, Wrong
End Class
```

but rather do write:

```csharp
public class SomeSpecialContainer
{
  private const int _maxItems = 32;
  private const int _highWaterMark = _maxItems * 0.75; // at 75%, Okay
}
```

```vbnet
Public Class SomeSpecialContainer
    Private Const _maxItems = 32
    Private Const _highWaterMark = _maxItems * 0.75 ' at 75%, Okay
End Class
```

Please note that an *enum* can often be used for certain types of symbolic constants. Make sure a constant value is a real constant (such as natural constants, e.g. the number of days in a week or the number of days in a year or leap year), if a public constant is changed all callees should be recompiled. Making an item a constant ensures that memory is only allocated once.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

### Fields

#### D040: Never declare fields external

Fields should never be declared external.

Exceptions: 
- Static readonly fields and constants, which may have any accessibility deemed appropriate.

Non-private fields do not use an underscore prefix, instead name it as a property (Pascal cased).

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

### Methods

#### D041: Use dynamic binding to differentiate between types

This is a general OO principle. Please note that it is usually a design error to write a selection statement that queries the type of an instance (keywords *typeof, is*).

Wrong example:

```csharp
static string GetSerializedObject(object objectToSerialize)  // Wrong
{
  if (objectToSerialize is ISimpleSerialize ss)
  {
    return ss.Serialize();
  }
  else if (objectToSerialize is IAdvancedSerialize as)
  {
    return as.Serialize(System.Threading.Thread.CurrentThread.CurrentCulture);
  }
  else
  {
    throw new ArgumentException("Object not serializeable", "objectToSerialize");
  }
}
```

```vbnet
Sub GetSerializedObject(objectToSerialize As Object) ' Wrong
    If TypeOf objectToSerialize Is ISimpleSerialize Then
        Console.WriteLine(CType(objectToSerialize, ISimpleSerialize).Serialize())
    ElseIf TypeOf objectToSerialize Is IAdvancedSerialize Then
        Console.WriteLine(CType(objectToSerialize, IAdvancedSerialize).Serialize(System.Threading.Thread.CurrentThread.CurrentCulture))
    Else
        Throw New ArgumentException("Object not serializeable", "objectToSerialize")
    End If
End Sub
```

Good example:

```csharp
static string GetSerializedObject(ISimpleSerialize objectToSerialize)
{
  return objectToSerialize.Serialize();
}

static string GetSerializedObject(IAdvancedSerialize objectToSerialize)
{
  return objectToSerialize.Serialize(System.Threading.Thread.CurrentThread.CurrentCulture);
}
```

```vbnet
Sub GetSerializedObject(objectToSerialize As ISimpleSerialize)
  Console.WriteLine(objectToSerialize.Serialize())
End Sub

Sub GetSerializedObject(objectToSerialize As IAdvancedSerialize)
  Console.WriteLine(objectToSerialize.Serialize(System.Threading.Thread.CurrentThread.CurrentCulture))
End Sub
```

Exceptions: 
- Using a selection statement to determine if some instances implements one or more **optional** interfaces is a valid construct though.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### D042: If you must provide the ability to override a method, make only the most complete overload virtual and define the other operations in terms of it

Using the pattern illustrated below requires a derived class to only override the virtual method. Since all the other methods are implemented by calling the most complete overload, they will automatically use the new implementation provided by the derived class.

```csharp
public class MultipleOverrideDemo
{
  private string _someText;

  public MultipleOverrideDemo(string s)
  {
    _someText = s;
  }

  public int IndexOf(string s)
  {
    return IndexOf(s, 0);
  }

  public int IndexOf(string s, int startIndex)
  {
    return IndexOf(s, startIndex, _someText.Length - startIndex);
  }

  public virtual int IndexOf(string s, int startIndex, int count)
  {
    return _someText.IndexOf(s, startIndex, count);
  }
}
```

```vbnet
Class MultipleOverrideDemo
    private _someText As String

    Public Sub New(s As String)
        _someText = s
    End Sub

    Public Function IndexOf(s As String) As Integer
        Return IndexOf(s, 0)
    End Function

    Public Function IndexOf(s As String, startIndex As Integer) As Integer
        Return IndexOf(s, startIndex, _someText.Length - startIndex)
    End Function

    Public Overridable Function IndexOf(s As String, startIndex As Integer, count As Integer) As Integer
        Return _someText.IndexOf(s, startIndex, count)
    End Function
End Class
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### D043: Do not use operator overloading

We recommend not to use operator overloading.

See [microsoft recommendations](http://msdn.microsoft.com/en-us/library/ms229032\(v=vs.110\).aspx) on the subject.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### D044: When to use a property instead of a method

A property should be considered when:

- Properties should behave as though they are fields.
- The member is a logical data member.
- The operation is not a conversion.
- The operation is not expensive. The user should not have to consider caching the results
- Obtaining a property value using the *get* accessor would have no observable side effect.
- Calling the member twice in succession produces the same results.
- Properties should be stateless with respect to other properties, i.e. there should not be an observable difference between first setting property A and then B and its reverse.

When using properties, consider using a propertychanged event instead of a changed event for every property. Also properties should only return errors on very exceptional cases. In a normal scenario a property should not return an error. If a property can only return a sensible number after a certain method is executed the return a default value or null until the method is called. Do not return an error. Properties can return errors after the instance is disposed.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### D045: Do not use properties to get or set an array of values

This causes property values to be modifiable outside of the containing class. Expose the values as a readonly type (contain data in readonly type) or use a method which exposes a clone of the data. Do not expose IEnumberable<>of the same array because it can just be casted back to the array and modified.

```csharp
class MyClass
{
    private string[] _data;

    // Wrong, result can be casted to string[]
    public IEnumerable<string> Data
    {
        get { return _data; } // Wrong, using _data.GetEnumerator would fix this
    }

    // Ok, but this can be slow so it should be a method instead of property
    public string[] GetData()
    {
        return (string[])_data.Clone();
    }

    // Good, result can only be read
    public ReadOnlyCollection<string> Data
    {
        get { return Array.AsReadOnly(_data); }
    }
}
```

```vbnet
Class MyClass
    Private _data As String()

    ' Wrong, result can be casted to string[]
    Public ReadOnly Property Data As IEnumerable(Of String)
        Get
            Return _data ' Wrong, using _data.GetEnumerator would fix this
        End Get
    End Property

    ' Ok, but this can be slow so it should be a method instead of property
    Public Function GetData() As String()
        Return _data.Clone()
    End Function

    ' Good, result can only be read
    Public ReadOnly Property Data As ReadOnlyCollection(Of String)
        Get
            Return Array.AsReadOnly(_data)
        End Get
    End Property
End Class
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

### Collections

#### D046: Do not return null (nothing in VB) from non-private properties or methods to indicate an empty collection or IEnumerable

Always return an empty collection type instead of null (nothing in VB), to facilitate the use of Linq and other methods without the need to check each collection for null. THis also prevents hard to spot bugs.

```csharp
private Node[] _children;

// Wrong, returns null when _children is null
public IEnumerable<Node> Children
{
  get { return _children?.GetEnumerator(); }
}

// OK
public IEnumerable<Node> Children
{
  get { return _children?.GetEnumerator() ?? Array.Empty<Node>(); }
}
```

```vbnet
private _children As Node()

' Wrong, returns nothing when _children is nothing
public ReadOnly Property Children As IEnumerable(Of Node)
    Get
        Return _children?.GetEnumerator()
    End Get
End Property

' Ok
public ReadOnly Property Children As IEnumerable(Of Node)
    Get
        Return If(_children?.GetEnumerator(), Array.Empty(Of Node()))
    End Get
End Property
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

### Namepace

#### D047: Name namespaces according to a predefined pattern and logical feature

<Company>.(<Product>|<Technology>)[.<Feature>][.<Subnamespace>]

- Prefix namespace names with a company name to prevent namespaces from different companies from having the same name.
- Use a stable, version-independent product name at the second level of a namespace name.
- DO NOT use organizational hierarchies as the basis for names in namespace hierarchies, because group names within corporations tend to be short-lived. Organize the hierarchy of namespaces around groups of related technologies
- Prefer Plural namespace names where appropriate. For example, use System.Collections instead of System.Collection. Brand names and acronyms are exceptions to this rule, however. For example, use System.IO instead of System.IOs.
- DO NOT use the same name for a namespace and a type in that namespace. This results in hard to use types.
- Consider using the same structure as Microsoft uses for their namespaces. This makes it easier for developers to located the code.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft|Same [see](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-namespaces)|
|Focus||
|Profit||

### Assemblies

#### D048: Name assemblies according to a predefined pattern and the large chunks of functionality

Start all assembly names with <Company>.<Product>. Seperate subparts with a dot(.). For example: Microsoft.Office.Excel.

Choose names for your assembly DLLs that suggest large chunks of functionality, such as System.Data.

Assembly and DLL names don’t have to correspond to namespace names, but it is reasonable to follow the namespace name when naming assemblies. A good rule of thumb is to name the DLL based on the common prefix of the namespaces contained in the assembly. For example, an assembly with two namespaces, MyCompany.MyTechnology.FirstFeature and MyCompany.MyTechnology.SecondFeature, could be called MyCompany.MyTechnology.dll.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft|Same [see](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-assemblies-and-dlls)|
|Focus||
|Profit||

## OO-principles

#### D049: It should be possible to use a reference to an instance of a derived class wherever a reference to that instances base class is used

This rule is known as the Liskov Substitution Principle, often abbreviated to LSP. Please note that an interface is also regarded as a base class in this context.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### D050: All variants of an overloaded method must be used for the same purpose and have similar behavior

Doing otherwise is against the Principle of Least Surprise. As a general rule you should only use overloads if the overloads call each other and have no additional behavior.

Example:

```csharp
private void MakePageActive(Page page)
{
  page.Visible = true;
  page.TopMost = true;
}

private void MakePageActive(int pageNumber)
{
  for (int i = 0; i < Pages.Length; i++)
  {
    if (i = pageNumber)
    {
      MakePageActive(Pages[i]);
    }
    else
    {
      Pages[i].Visible = false; // Wrong, because of this extra behavior
    }
  }
}
```
```vbnet
Sub MakePageActive(page As Page)
    page.Visible = True
    page.TopMost = True
End Sub

Sub MakePageActive(pageNumber As Integer)
    For i As Integer = 0 To Pages.Length - 1
        If i = pageNumber Then
            MakePageActive(Pages(i))
        Else
            Pages(i).Visible = False
        End If
    Next
End Sub
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

# Maintainability

## General

#### M001: Do not mix code from different providers in a single file

In general, third party code (e.g. code generated by Visual Studio) will not comply with the coding standards, so do not put such code in the same file as proprietary code or make sure it conforms.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### M002: Declare and initialize variables close to where they are used

This is a best practice so code can be refactored or spit into new functions easy. 

Exceptions: 
- This does not apply to class level variables.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendationule|
|Microsoft||
|Focus||
|Profit||

#### M003: If possible, initialize variables at the point of declaration

If you use field initialization then instance fields will be initialized before the instance constructor is called. Likewise, static fields are initialized when the static constructor is called or before first use. Notice that the compiler will always initialize any uninitialized value type variable to zero and reference type variable to null (nothing in VB) Also note that it is perfectly allowed to use the ? : operator (if(x,y,z) in VB).

```csharp
enum Color
{
  Red,
  Green
}

bool _fatalSituation = IsFatalSituation();
Color _backgroundColor = _fatalSituation ? Color.Red : Color.Green;
```

```vbnet
Enum Color
    Red
    Green
End Enum

Dim _fatalSituation = IsFatalSituation()
Dim _backgroundColor = If(_fatalSituation, Color.Red, Color.Green)
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### M004: Do not access a modified instance more than once in an expression (C# only)

The evaluation order of sub-expressions within an expression is defined, but such code is hard to understand.

Example:

```csharp
int x = 0, y = 0, z = 0;
var list = new List<int>() { x, y };

list[x] = x++; // Wrong, hard to read (is list[0] or list[1] set and with 1 or 2)
y = y++ + 2; // Wrong, hard to read (is y 2 or 3), consider using y += 2
z = ++z + 2; // Wrong, hard to read (is z 2 or 3), consider using z += 3
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

## Wrapping and indentation

#### M005: Use two spaces indention for code files

Use two spaces of indentation (no tabs).

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft|Uses 4 spaces, [see](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)|
|Focus||
|Profit||

#### M006: Left-align function arguments when wrapping the function call

Function calls can be wrapped when they become too large. Always wrap right of the bracket of the previous line. When wrapping a function call, left-align the arguments:

```csharp
CreateFile(configFilePath, overwrite, 
           encoding);
```

```vbnet
CreateFile(configFilePath, overwrite, _
           encoding);
```

Exceptions:
- If there is still no room because of the length of the function name, then use two indents (4 spaces) compared to the line which starts the statement.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### M007: Left-align wrapped conditions

Composite conditions calls can be wrapped when they become too large. Always indent the new line by two indents (4 spaces) compared to the line which starts the statement. Start the new line with a binary operator:

```csharp
if (!File.Exists(filePath) || thisIsABool
    || overwrite)
{
}
```

```vbnet
If !File.Exists(filePath) || thisIsABool _
    || overwrite Then
  ...
End If
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft|Same [see](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)|
|Focus||
|Profit||

## .NET Language constructs

#### M008: Never use this. (Me. in VB)

Never use the *this*. (Me. in VB) construction except if needed to distinguish types from members.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### M009: Do not make explicit comparisons to true or false

It is usually bad style to compare a bool-type expression to true or false.

Example:

```csharp
while (condition == false) // Wrong; bad style
while (condition != true) // also Wrong
while (((condition == true) == true) == true) // where do you stop? (Wrong)
while (condition)  // OK
while (!condition) // OK
```

```vbnet
While condition = false ' Wrong; bad style
While condition <> true ' also Wrong
While (((condition = true) = true) = true) ' where do you stop? (Wrong)
While condition  ' OK
While Not condition ' OK
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### M010: Do not use LINQ Query language syntax

Use the method syntax instead of the query syntax. The method syntax is more flexible and can be used in more scenarios. The query syntax is just a wrapper around the method syntax.

Example:

```csharp
var query = from c in customers
            where c.City == "London"; // Wrong

var query = customers.Where(c => c.City == "London"); // OK
```

```vbnet
Dim query = From c In customers
            Where c.City = "London" ' Wrong

Dim query = customers.Where(Function(c) c.City = "London") ' OK
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### M011: Rethrow without parameters to preserve stack details

Once an exception is thrown, part of the information it carries is the stack trace. The stack trace is a list of the method call hierarchy that starts with the method that throws the exception and ends with the method that catches the exception. If an exception is re-thrown by specifying the exception in the throw statement, the stack trace is restarted at the current method and the list of method calls between the original method that threw the exception and the current method is lost. To keep the original stack trace information with the exception, use the throw statement without specifying the exception.

```csharp
try
{
  throw new MsgException(“error”);
}
catch(MsgException ex)
{
  //DoStuff
  throw ex; // Wrong, do not use the exception as a parameter when rethrowing.
}

try
{
  throw new MsgException(“error”);
}
catch(MsgException ex)
{
  //DoStuff
  throw; // Ok
}
```

```vbnet
Try
    Throw New MsgException("error")
Catch ex As MsgException
    'DoStuff
    Throw ex '  Wrong, do not use the exception as a parameter when rethrowing.
End Try

Try
    Throw New MsgException("error")
Catch ex As MsgException
    'DoStuff
    Throw ' Ok
End Try
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### M012: Do not use an explicit Type declaration for Generic methods that have a parameter of the same generic type

Generic methods that have generic parameter(s) of the same generic type that is used in the methods declaration can be used without explicitly specifying the generic type, this type is derived from the value provided to the parameter.

```csharp
public void Add<T>(T itemToAdd) { } // Generic method.

int i = 9;
Add<int>(i);     // Wrong, Explicit type declaration is used.
Add(i);          // Ok, No explicit type declaration, type is derived from the provided parameter.
```

```vbnet
Sub Add(Of T)(item As T)
End Sub

Dim i = 0
Add(Of Integer)(i)  ' Wrong, Explicit type declaration is used.
Add(i)              ' Ok, No explicit type declaration, type is derived from the provided parameter.
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### M013: Prefer || over | (OrElse over Or in VB) and && over & (AndAlso over And in VB)

The || and && are short circuit operators. This can be a performance gain.

Example:

```csharp
If (a != null && a.Value == 5)
  'Do something
End If
```

```vbnet
If a IsNot Nothing AndAlso a.Value = 5 Then
  'Do something
End If
```

The & operator would evaluate both expressions, even if the first expression is False. The && operator would only evaluate the second expression if the first expression is True. This will also prevent NullReferenceExceptions.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft|Same [see](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)|
|Focus||
|Profit||

#### M014: Use prefered string comparison and sorting methods

- DO: Use StringComparison.Ordinal or OrdinalIgnoreCase for comparisons as your safe default for culture-agnostic string matching.
- DO: Use StringComparison.Ordinal and OrdinalIgnoreCase comparisons for increased speed.
- DO: Use StringComparison.CurrentCulture-based string operations when displaying the output to the user.
- DO: Switch current use of string operations based on the invariant culture to use the non-linguistic StringComparison.Ordinal or StringComparison.OrdinalIgnoreCase when the comparison is linguistically irrelevant (symbolic, for example).
- DO: Use ToUpperInvariant rather than ToLowerInvariant when normalizing strings for comparison.
- DON'T: Use overloads for string operations that don't explicitly or implicitly specify the string comparison mechanism.
- DON'T: Use StringComparison.InvariantCulture-based string operations in most cases; one of the few exceptions would be persisting linguistically meaningful but culturally-agnostic data.

String.Compare better shows your intend than ToUpper. ToUpper is faster than ToLower. Also some languages (cultures) use different captitalization and sorting than others, for example the ẞ vs ss in german and the Turkish-I Problem. So specifing Ordinal, Invariant or culture will show better intent in code.

So a name with a ẞ will be shown at a different position in german application (for example phone book) than then in a dutch application.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft|Same [see](https://learn.microsoft.com/en-us/previous-versions/dotnet/articles/ms973919(v=msdn.10)?redirectedfrom=MSDN)|
|Focus||
|Profit||


#### M015: Use XML comments for describing methods, classes, fields, and all public members

Can be picked up by tools such as visual studio to provide intellisense and help generations.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft|Same [see](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)|
|Focus||
|Profit||

#### M016: Provide clean comments

- Use single-line comments (//) for brief explanations.
- Avoid multi-line comments (/* */) for longer explanations. Comments aren't localized. Instead, longer explanations are in the companion article.
- Begin comment text with an uppercase letter. 
- End comment text with a period.
- Insert one space between the comment delimiter (//) and the comment text, as shown in the following example.

```csharp
// The following declaration creates a query. It does not run
// the query.
```

```vbnet
' The following declaration creates a query. It does not run
' the query.
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft|Same [see](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)|
|Focus||
|Profit||

## C# only Language constructs

#### M017: Use a direct cast when you know the cast never fails else use as or is

If you use as then you will always have to check for null.

Example:

```csharp
var c = (ICalc)RemotingServices.Connect(typeof(ICalc), @"http://localhost:999/demo");  //Ok because return type is known

bool valid = true;

foreach(Control ctrl in Controls)
{
  var actrl = ctrl as AzureControl; //use as because cast can fail

  if(actrl != null)
  {
    valid = actrl.IsValid;
    break;
  }
}
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### M018: A block must always start on a new line. A statement end (;) may never be followed by a new statement on the same line.

This increases readability and can sometimes prevent accidental errors caused by inaccurate cut/paste operations, because of a dangling curly bracket. 

Please note that this also avoids possible confusion in statements of the form:

```csharp
// Wrong
if (b1) Foo(); else Bar(); 

// Wrong
if(b1) { Foo(); } else Bar(); 

// OK
if(b1)
{ 
  Foo();
} 
else
{
  Bar();
}
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### M019: Prefer `is null` over `== null` and `is not null` over `!= null` when explicitly comparing to null

The == and != are overloadable operators the is operator. The is operator is a language construct. 

Please not that this only applies when explicitly comparing and not when we compare variable values.

```csharp
if(x is null) // Ok
{
  if(y == null) // Wrong, is null would be prefered
  {
    if(x == y) // Ok, variable compare
    { }
  }
}
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### M020: Turn on nullable reference types compiler mode

`NullReferenceException` is one of the most common runtime exceptions thrown. The Nullable compiler mode lets you be more specific on your intend and requires you to use the `!` operator when you know a reference type can never be null. Always set WarningsAsErrors to Nullable so these warnings need to be resolved before running the application.

You can temporary turn off the nullable context in a (part of) a file by using:

```csharp
#nullable enable //Enables nullable context.
#nullable disable // Disables nullable context.
#nullable restore //Restores the nullable context to the project default.
```

Note that nullable reference types is a compiler feature. At .NET level all classes still support null and there is no different type for nullable reference types, as there is for nullable structs (Nullable<T>). So at runtime there are no rules enforced and languages without Nullable reference types (example VB.NET) will be albe to set some values to null, so for reference types you shoulds still implement null checks.

Set the following options in your profject file (or Directory.Build.props).

```xml
<Nullable>enable</Nullable>
<WarningsAsErrors>Nullable</WarningsAsErrors>
```

More info:
https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references
https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/errors-warnings

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

## VB.NET only Language constructs

#### M021: Use DirectCast instead of CType()

DirectCast is faster and more specific than CType, also it will not let you lose precision.

```vbnet
Dim newint As Integer = DirectCast(3345.34, Integer) 'Compiler Error: Option Strict On disallows implicit conversions from 'Double' to 'Integer'.
Dim newint2 As Integer = CType(3345.34, Integer) 'Will compile
```

Use DirectCast when you are sure the cast will succeed else use TryCast() and check for Nothing.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### M022: Turn on Option Strict, Option Explicit and Option Infer

Always turn on Option Strict, Option Explicit and Option Infer in the project settings. This will prevent a lot of errors and will make the code more readable.


```xml
<OptionExplicit>On</OptionExplicit>
<OptionInfer>On</OptionInfer>
<OptionStrict>On</OptionStrict>
```

More info:

https://learn.microsoft.com/en-us/dotnet/visual-basic/language-reference/statements/option-explicit-statement
https://learn.microsoft.com/en-us/dotnet/visual-basic/language-reference/statements/option-infer-statement
https://learn.microsoft.com/en-us/dotnet/visual-basic/language-reference/statements/option-strict-statement

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### M023: Do not use On Error Goto

Use the Try...Catch and Using Statements when you use Exception Handling.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### M024: Use the IsNot Keyword

Use ... IsNot Nothing instead of Not ... Is Nothing.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### M025: Do not use static method level variables

Prefer class level variable. Static method level variables are often misunderstood which leads to hard to spot bugs.

```vbnet
Private Sub DoSomething()
  Static counter As Integer 'Wrong, use class / module level variable

  ...
End Function
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

## Source Control

#### M026: Do not move or change code bases on personal preference

Source control stores changes bases on deltas which contain added and deleted lines, not moved. Moving lines will increase the chance of merge conflicts and will prevent auto-merge. It makes it more difficult to track changes over time using the history view. Don’t change identifiers based on personal preference, but do change them to correct typos or if the contents of the identifier does not properly reflect the name anymore. When in doubt discuss the proposed change with at least one other (lead) developer.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### 7: Check-in frequently

The sooner you check-in your changes the earlier others can merge your code. This prevents merge conflicts. You do not have to wait for everything to be working before you check-in. As long as you don’t break existing functionality you can check-in. In feature branches you can even break existing functionality as long as you communicate what you break to other team members working in the branch. Rule of thumb: Daily, but at least once a week.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### M028: When moves or renames are part of the check-in, pay close attention to the path that the tool recommends

Move and rename can only be tracked if executed by the proper tools. If improperly tracked they cause merge conflicts which are very hard to resolve. Check your pending changes window to make sure your change was properly tracked.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### M029: Add new functions below the function which calls them

This makes changes better readable and prevents merge conflicts. The chance two users are rewriting the same function are much smaller than the change two users are adding functions to the same file.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### M030: Plan rename refactoring with large code churn and merge them separately to master

If you have a small change which affects lots of files release that change separately from other changes. Example: if you want to change the name of a function, class or move a class it to another namespace and you notice that it affects lots of files that the chances and complexity of merge conflicts increase exponentially. You can choose release the name change directly to master and send an email to all other branch owners to pull as soon as possible. This allows all other owners to merge the change and resolve the relative easy merge conflicts without dealing with the actual program structure changes. Another option is to postpone the rename until after you branch is merged to master and then do the name change as part of a separate check-in to master. Also send an email to all other branch owners. This allows other branch owners to first pull the code change (not latest, but up to specific change set), merge then and then pull the rename. This will also prevent complex merges.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### M031: If you checkin a fix strongly consider adding documentation in source

The fact that you made a bug you probably did not consider a scenario or the construction was more complex than you initially thought. You should document that scenario in code or add some comments describing the fixed lines in more details. This prevents you from forgetting the scenario the next fix and allows others who read the code to also have the same knowledge. 

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

# Performance

## .NET

#### P001: Use a StringBuilder for string construction in iterations

Strings are immutable, which means that when you concatenate two strings to each other, you effectively create a new string and copy the contents of the other two into it. The more strings are concatenated, the more copying is performed which may result in a dramatic performance loss. When a string is construction in some sort of iteration, create a StringBuilder instance to construct the string:

```csharp
private static string GetSqlSelect(Fields fields)
{
  StringBuilder sql = new StringBuilder("SELECT ");

  foreach(Field field in fields)
  {
    sql.Append(field.Name);
    sql.Append(",")
  }

  return sql.ToString(0, sql.Length - 1);
}
```
```vbnet
Private Function GetSqlSelect(fields As Fields) As String
    Dim sql As New StringBuilder("SELECT ")

    For Each field As Field In fields
        sql.Append(field.Name)
        sql.Append(",")
    Next

    Return sql.ToString(0, sql.Length - 1)
End Function
```

When constructing a string of elements in a string array, consider using the static *string.Join* method:

```csharp
private static string GetSqlSelect(string[] fields)
{
  return $"SELECT {string.Join(",", fields)}";
}
```

```vbnet
Private Function GetSqlSelect(fields As String()) As String
    Return $"SELECT {String.Join(",", fields)}"
End Function
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### P002: Use string interpolation to concatenate string constants, variables or literals

When concatenating string constants, variables or literals, use String interpolation for best performance. For example:

```csharp
public static string GetErrorMessage(string message)
{
  return $"Error '{message}' occurred at {DateTime.Now:G}";
}
```

```vbnet
Public Function GetErrorMessage(message As String) As String
    Return $"Error '{message}' occurred at {DateTime.Now:G}"
End Function
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### P003: Only throw exceptions in exceptional cases

You should only throw exceptions when a condition outside of your code's assumptions occurs. In other words, you should not use exceptions as a means to provide your intended functionality. For example, a user might enter an invalid user name or password while logging on to an application. While this is not a successful logon, it should be a valid and expected result, and therefore should not throw an exception. However, an exception should be generated if an unexpected condition occurs, such as an unavailable user database. Throwing exceptions is more expensive than simply returning a result to a caller. Therefore, exceptions should not be used to control the normal flow of execution through your code. In addition, excessive use of exceptions can create unreadable and unmanageable code.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

## WPF

# UI - UX

## WPF

#### U001: Designer types should be postfixed with the type

Designer types may be post fixed with the Designer type. Designer types should not start with underscore. If the name becomes very long abbreviations is allowed.

Examples:
```
_nameTextBox // Wrong, starts with _
textBoxName // Wrong, prefixed with designer type
nameTextBox // OK

isInvoicingSelectedDataGridCheckBoxColumn // OK
```

|**Team**|**Status**|
| :- | :- |
|Initial advise|Rule|
|Microsoft||
|Focus||
|Profit||

#### U002: Turn on Snaptodevicepixels in all forms

Set it to true on the root element of every form. For devices operating at greater than 96 dots per inch (dpi), pixel snap rendering can minimize anti-aliasing visual artifacts in the vicinity of single-unit solid lines.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### U003: Set RenderOptions.BitmapScalingMode to NearestNeighbor for sharper icon and image edges

To improve sharpness of images.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### U004: Set TextOptions.TextFormattingMode to Display when font size <= 15

To improve text sharpness on small fonts.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### U005: Set VirtualizingStackPanel.VirtualizationMode to Recycling when visual tree contains VirtualizingStackPanel

By default, a VirtualizingStackPanel creates an item container for each visible item and discards it when it is no longer needed (such as when the item is scrolled out of view). When an ItemsControl contains a lot of items, the process of creating and discarding item containers can negatively affect performance. When VirtualizingStackPanel.VirtualizationMode is set to Recycling, the VirtualizingStackPanel reuses item containers instead of creating a new one each time.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### U006: Set x:SynchronousMode to Async when loading large xaml

This improves loading time. See [XamlReader.LoadAsync Method](https://msdn.microsoft.com/en-us/library/aa346593\(v=vs.100\).aspx)

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### U007: Set Binding IsAsync on when loading slow properties

This allows loading while not freezing UI thread.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### U008: Favor StaticResources over DynamicResources

StaticResources provide values for any XAML property attribute by looking up a reference to an already defined resource. Lookup behavior for that resource is the same as a compile-time lookup. DynamicResources will create a temporary expression and defer lookup for resources until the requested resource value is required. Lookup behavior for that resource is the same as a run-time lookup, which imposes a performance impact. Always use a StaticResource whenever possible. 

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### U009: Opacity on Brushes Instead of Elements

If you use a Brush to set the Fill or Stroke of an element, it is better to set the Opacity on the Brush rather than setting the element’s Opacity property. When you modify an element’s Opacity property, it can cause WPF to create temporary surfaces which results in a performance hit. 

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### U010: Favor StreamGeometries over PathGeometries

The StreamGeometry type is a very light-weight alternative to a PathGeometry. StreamGeometry is optimized for handling many PathGeometry types. It consumes less memory and performs much better when compared to using many PathGeometry types. 

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### U011: Use and Freeze Freezables

A Freezable is a special type that has two states: unfrozen and frozen. When you freeze an instance such as a Brush or Geometry, it can no longer be modified. Freezing instances whenever possible improves the performance of your application and reduces its memory consumption. 

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||

#### U012: Fix your Binding Errors

Binding errors are the most common type of performance problem in WPF apps. Every time a binding error occurs, your app takes a perf hit and as it tries to resolve the binding and writes the error out to the trace log. As you can imagine, the more binding errors you have the bigger the performance hit your app will take. Take the time to find and fix all your binding errors. Using a RelativeSource binding in DataTemplates is a major culprit in binding error as the binding is usually not resolved properly until the DataTempate has completed its initialization. Avoid using RelativeSource.FindAncestor at all costs. Instead, define an attached property and use property inheritance to push values down the visual tree instead of looking up the visual tree.

|**Team**|**Status**|
| :- | :- |
|Initial advise|Recommendation|
|Microsoft||
|Focus||
|Profit||
