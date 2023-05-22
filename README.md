

<!-- Anchor for the "back to top" links -->
<a id="readme-top"></a>

<!-- Project logo -->
<br />
<div align="center">
  <a href="#">
    (Insert fancy logo here)
  </a>
  <h1>Unity save system example</h1>
</div>

> Note this project is an example, and does not represent a fully working save system.
> It is meant to help users looking for a way to save data, as it shows one of the many ways to do so.

<!-- Table of contents -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#getting-started">Getting Started</a>
    </li>
    <li>
      <a href="#using-the-project">Using the project</a>
    </li>
    <li>
      <a href="#issues">Issues</a>
    </li>
	<li>
      <a href="#todo">Todo</a>
    </li>
  </ol>
</details>



<!-- Getting started -->
## Getting Started

### Requirements
- Unity editor version `2021.3.16f` (this is the latest LTS version as of writing this README).

### Getting the project running
This is a step-by-step instruction on how to run the project.
1. Clone the repository: `https://github.com/PillowCoding/UnityExamples-SaveSystem.git`
2. Open `src/SaveSystemExample` in Unity.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- Using the project -->
## Using the project

### Glossary
_Singleton_: A term used to indicate that only a single instance is allowed for that given class. In unity you very often see a static "instance" of sorts that references that single instance.

_SaveManager_: The singleton monobehaviour that is responsible for keeping track of saveable classes, and saving/receiving data to/from persistent data.

_PositionableObjectManager_: The example manager that randomly spawns objects which will then be saved by the `SaveManager`.

_ISaveable_: The interface a class is required to implement in order to be eligible for registering inside the `SaveManager`

_Saveable_: A class that is created during the deserialization process, containing the unique key and data for a saveable class.

### Folder structure
_Editor_: Editor specific scripts. This is currently unused.

_Runtime_: Runtime specific scripts. This contains the main framework of the save system, including the `SaveManager`.

_Samples_: Examples. This contains an example scene and scripts to make the save system working.

### Usage
##### Play the scene.
Notice the UI will have buttons drawn that allow you to test the behaviour of the save system. The top button spawns a square with a random position and rotation. The middle button saves the current spawned squares. The bottom button loads the squares into the scene.

##### Registering a class to be saved.
As mentioned above, a class must implement `ISaveable` to be eligible to register. This interface implements two methods, and a property:
- `string Key`: A property that acts as a unique key. This is saved alongside the data and later used to determine what class requires what data.
- `object ToSaveable()`: Collects the data to be saved.
- `void FromSaveable(JContainer? @object)`: Returns the saved data to the class.

The `SaveManager` has two methods regarding registering:
- `void Register<TSaveable>(TSaveable saveable)`: Registers the class to be saved. `saveable` has a constraint and requires the class to inherit from `MonoBehaviour` and implement `ISaveable`.
- `void UnRegister<TSaveable>(TSaveable saveable)`: Unregisters the class and no longer saves it. `saveable` has the same constrains as above.
- `bool HasRegistered<TSaveable>(TSaveable saveable)`: Returns `true` if the class has been registered to be saved. `saveable` has the same constrains as above.

Saving your class is simple. `SaveManager` has two methods:
- `void StartSave()`: Starts the save process. This method will call `ToSaveable` on all the saveable classes in order to collect data.
- `void StartLoad()`: Starts the loading process. This method will call `FromSaveable` on all the saveable classes in order for them to use the now loaded data.

Data is stored in [Application.persistentDataPath](https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html)/save.json

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- Issues -->
## Issues
- I suppose one issue would be that this system requires the saveable classes to be registered quite early, and this might not always be the case if you haven't loaded certain scenes.

## Todo
- Maybe find a way to get rid of `JContainer` in `FromSaveable()`.
- Remove the `Monobehaviour` constraint from the `SaveManager`.
