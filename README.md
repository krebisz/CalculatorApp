# Calculator

These are just some quick notes on the project.

0. Based on the App requirements, a WPF c# .Net Core Framework was chosen. Set the Calculator.Application project as the startup project in the solution.
1. Some efforts using DI for the Calculator class have been made, helping to separate the Front-End from the implementation logic
2. Front-End is simple XAML and additional buttons (functions/operations) can be easily added
3. Operators/Operations can be easily added to, but could also be abstracted further
4. Events in the MainWindow have some control logic extracted into it's own regions sesction. This can be moved to it's own file and class. Some components could also be injected
5. Some of the keyboard controls for the calculator need some further testing and work to make them function 100%
6. Styling could be enhanced with ControlTemplates as well
7. Online resources were refernced to do things like binding to correct keys, or to verify the processes created
8. Unit tests could also have been added
9. I hope this serves as a general indication of the approach(es) I take with respect to development and design
