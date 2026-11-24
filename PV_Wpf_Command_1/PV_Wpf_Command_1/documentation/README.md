# PV_Wpf_Command_1

A minimal WPF (.NET Framework 4.8) application that demonstrates the **MVVM pattern** and the **Command pattern** (`ICommand`). It shows a window with one button; clicking it displays a "Hello World!" message box.

The point of the project is not the message box. It is *how* the click gets there: the button has no click handler in code-behind. It is wired to a command on a ViewModel through data binding.

---

## 1. What the program does

1. The app starts and opens `MainWindow`.
2. The window contains one button labelled **Click Me**.
3. Clicking it shows a `MessageBox` saying **Hello World!**.

---

## 2. Project structure

```
PV_Wpf_Command_1/
├── App.xaml / App.xaml.cs        Application entry point (StartupUri → MainWindow)
├── App.config                    Targets .NET Framework 4.8
├── MainWindow.xaml               The View (UI markup)
├── MainWindow.xaml.cs            Code-behind: creates the ViewModel and sets DataContext
├── Commands/
│   └── RelayCommand.cs           Reusable ICommand implementation
├── ViewModel/
│   ├── ViewModelBase.cs          Base class for ViewModels (property-change helper)
│   └── ViewModel.cs              The ViewModel exposing ShowMessageBoxCommand
├── Interfaces/                   (empty, reserved)
├── Services/                     (empty, reserved)
├── Properties/                   Auto-generated resources/settings/assembly info
└── documentation/README.md       This file
```

`bin/`, `obj/` and `.vs/` are build output and Visual Studio state; they are not part of the source.

---

## 3. Architecture: MVVM

| Layer | File(s) | Responsibility |
|---|---|---|
| **View** | `MainWindow.xaml` | Declares the UI. Knows *what* it can bind to, but contains no logic. |
| **ViewModel** | `ViewModel/ViewModel.cs` | Exposes commands and state the View binds to. Contains the logic. |
| **Command** | `Commands/RelayCommand.cs` | Adapter that lets a ViewModel method be used as an `ICommand`. |
| **Model** | *(none yet)* | There is no data in this example. |

**Why MVVM?** It separates what the UI looks like from what it does. The ViewModel has no reference to any button or window, so it can be unit-tested and the UI can be redesigned without touching the logic.

---

## 4. How it works, step by step

### 4.1 Startup: `App.xaml`

```xml
<Application ... StartupUri="MainWindow.xaml">
```

`StartupUri` tells WPF to create and show `MainWindow` as soon as the application starts. The build generates a `Main()` method from `App.xaml` (the `ApplicationDefinition` item in the `.csproj`), so there is no hand-written entry point.

### 4.2 Connecting View and ViewModel: `MainWindow.xaml.cs`

```csharp
public MainWindow()
{
    InitializeComponent();
    DataContext = new ViewModel.ViewModel();
}
```

- `InitializeComponent()` loads the compiled XAML and builds the controls.
- `DataContext` is the object that all `{Binding ...}` expressions in the window resolve against. Setting it to a new `ViewModel` is what makes the bindings find `ShowMessageBoxCommand`.

This is the only code-behind in the project, and it is only wiring.

### 4.3 The binding: `MainWindow.xaml`

```xml
<Button Content="Click Me" Width="100" Height="30"
        Command="{Binding ShowMessageBoxCommand}" />
```

`Button` has a `Command` property of type `ICommand`. `{Binding ShowMessageBoxCommand}` tells WPF to look up a property of that name on the `DataContext` (the ViewModel). When the button is clicked, WPF calls `Execute()` on whatever command it finds there. Before that, it also calls `CanExecute()` to decide whether the button should be enabled.

### 4.4 The ViewModel: `ViewModel/ViewModel.cs`

```csharp
public ICommand ShowMessageBoxCommand { get; set; }

public ViewModel()
{
    ShowMessageBoxCommand = new RelayCommand(_ => ShowMessageBox());
}

private void ShowMessageBox()
{
    MessageBox.Show("Hello World!");
}
```

- The property is typed as `ICommand`, which is all WPF needs. The View doesn't know or care that it's a `RelayCommand`.
- The command is created in the constructor. The lambda `_ => ShowMessageBox()` ignores the command parameter (the `_`) and calls the private method.
- The method with the actual behaviour is private; only the command is public. The View can *trigger* the action but can't call it directly.

### 4.5 The command: `Commands/RelayCommand.cs`

`ICommand` requires three members, and `RelayCommand` implements them by delegating to functions supplied by the caller:

| Member | Purpose | In this class |
|---|---|---|
| `Execute(object)` | Run the action | Calls the `Action<object>` passed to the constructor |
| `CanExecute(object)` | Is the action currently allowed? WPF uses this to enable/disable the control | Currently always returns `true` |
| `CanExecuteChanged` | Event that tells WPF to re-ask `CanExecute` | Declared, never raised |

**Why a RelayCommand?** Implementing `ICommand` as a separate class for every action would be repetitive. `RelayCommand` is a generic wrapper: you hand it a method (and optionally a condition), and it behaves as an `ICommand`. It can be reused for every command in the app.

The constructor throws `ArgumentNullException` if `execute` is null, so a command can never be created that would fail when clicked.

### 4.5.1 Full flow of a click

```
User clicks button
   │
   ▼
Button (View) ──► Command binding resolves ShowMessageBoxCommand on DataContext
   │
   ▼
RelayCommand.CanExecute(...)  → true, so the button is enabled
   │
   ▼
RelayCommand.Execute(...)
   │
   ▼
ViewModel.ShowMessageBox()
   │
   ▼
MessageBox.Show("Hello World!")
```

### 4.6 `ViewModelBase`

```csharp
internal class ViewModelBase
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) { ... }
}
```

This is intended as a shared base class for all ViewModels (the original Hungarian comment says it is a base class for ViewModels that other programs can reuse). `OnPropertyChanged` raises the `PropertyChanged` event so that bound UI elements can refresh when a property changes.

It isn't used in this example, because the ViewModel has no changing state yet. It is groundwork for later steps.

---

## 5. Known limitations and things to be aware of

These don't affect the current demo, but they matter as soon as the project grows.

1. **`RelayCommand` ignores its `canExecute` parameter.**
   `CanExecute` always returns `true`, even if a `canExecute` function was supplied. The idiomatic body is:
   ```csharp
   public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
   ```

2. **`CanExecuteChanged` is never raised.**
   Even once `CanExecute` is fixed, WPF won't re-evaluate it when state changes. The usual approach is to forward the event to `CommandManager.RequerySuggested`:
   ```csharp
   public event EventHandler CanExecuteChanged
   {
       add    { CommandManager.RequerySuggested += value; }
       remove { CommandManager.RequerySuggested -= value; }
   }
   ```

3. **`ViewModelBase` does not implement `INotifyPropertyChanged`.**
   It declares a `PropertyChanged` event with the right signature, but WPF only listens to objects that implement the interface. Change the declaration to:
   ```csharp
   internal class ViewModelBase : INotifyPropertyChanged
   ```
   Without this, calling `OnPropertyChanged` has no effect on the UI. Optionally add `[CallerMemberName]` to `OnPropertyChanged(string propertyName = null)` to avoid typing property names as strings.

4. **`ViewModel` and `ViewModelBase` are `internal`.**
   That's fine for the XAML-side binding (which uses reflection on the instance), but it means the types can't be reused from another assembly. This conflicts with the comment on `ViewModelBase` saying other programs may reuse it; make it `public` if that's the intent.

5. **`ShowMessageBox` calls `MessageBox.Show` directly.**
   This ties the ViewModel to a UI class and makes it hard to unit-test. The empty `Interfaces/` and `Services/` folders suggest the planned fix: define a dialog service interface, implement it in `Services/`, and inject it into the ViewModel.

6. **The window title is the default `MainWindow`**, and the namespace `PV_Wpf_Command_1.ViewModel` contains a class also named `ViewModel`, which is why `MainWindow.xaml.cs` has to write `ViewModel.ViewModel`. Renaming the class (for example `MainViewModel`) would avoid this.

---

## 6. Build and run

**Requirements:** Windows, Visual Studio 2017 or later (the `.vs/v17` folder indicates Visual Studio 2022), .NET Framework 4.8 targeting pack.

1. Open `PV_Wpf_Command_1.sln`.
2. Build (`Ctrl+Shift+B`).
3. Run (`F5`). Click **Click Me**.

---

## 7. Key concepts demonstrated

- **MVVM**: separation of UI, logic and data.
- **Data binding**: the XAML `{Binding}` extension connects the View to ViewModel properties via `DataContext`.
- **Command pattern (`ICommand`)**: user actions are represented as objects, so the UI doesn't need event handlers.
- **`RelayCommand`**: a reusable adapter that turns a method into an `ICommand`.
