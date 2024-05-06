## Example Plugin System for InfoPanel

InfoPanelApp is made to represent your main app. You can keep this private if you wish (though I think it would be better open sourced)  
InfoPanel.Contract is the library that MUST be made public. Both InfoPanel and any plugins created will reference the Contract.  
It defines the interface and properties that mus be added.  
The Example.Plugin is just an example of a plugin. While there's only simple logic in there, it's enough to give an example.  

The InfoPanelApp adds a reference to the `Prise` nuget package, as well as a Project Reference to `InfoPanel.Contract`.  
The Example.Plugin adds a reference to the `Prise.Plugin` nuget package, as well as a project reference to `InfoPanel.Contract`.  

People can build their plugins off of the Contract library, then put their plugins in the plugins folder relative to the main app.
