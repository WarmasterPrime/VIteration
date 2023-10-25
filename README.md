# VIteration

<details>

<summary>About</summary>

### What Is VIteration?

`VIteration` is a C# class library that offers lambda iteration extension methods for classes that inherit from the `IEnumerable` interface.

### How To Use?

Below shows an example of how to implement `VIteration` into your code.
```cs
string[] paths = { "C:\Users\Example.txt", "C:\Users\User\Desktop\helloWorld.txt" };
PathInfo[] pathInfoArray = paths.Iterate(string filePath => new PathInfo(filePath));
```

</details>

<details>

 <summary>Features</summary>

 ### Object Extension Methods

|Method Name|Details|Example|
|---|---|---|
|Iterate|Synchronously iterates through a collection and returns an array of the results.|`collection.Iterate(Func<TIn, TOut>predicate);`|
|IterateMultiThread|Synchronously iterates through a collection using multi-threading techniques.|`collection.IterateMultiThread(Func<TIn, TOut>predicate);`|
|IterateAsync|Asynchronously iterates through a collection with multi-threading techniques (Accepts both synchronous and asynchronous predicate functions).|`collection.IterateAsync(Func<TIn, TOut>predicate);`|

When using multi-threading, the maximum number of threads used in the operation is determined by the number of processors the machine has.

</details>
