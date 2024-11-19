
Each module is separated by folders, for instance:

Core has a console app that is the entry point of the application.
Domain has the domain entities and interfaces.

Public is the shared interface to the rest of the modules, and it is the only module that is allowed to reference other modules.
