# xstream-dot-net

Introduction
XStream.Net is the .Net version of Joe Walnes' XStream for Java. It is a simple library for serialisation/deserialisation to & from xml. The original code for this project was taken from Arne Vandamme's weblog which was a port of XStream for Java.

Usage
```c#
string serialisedObject = new XStream().ToXml(originalObject);
object deserialisedObject = new XStream().FromXml(serialisedObject);
Assert.AreEqual(originalObject, deserialisedObject);
```
Example
say you have this class:

```c#
class SimpleClass
    {
      private int a; 
      private string x; 
    }
```    
    
and you execute this code:

```c#
    XStream xstream = new XStream(); 
    SimpleClass simp = new SimpleClass(1, 2, "testString");   
    System.Console.WriteLine(xstream.ToXml(simp)); 
```
you get this:

```xml
<SimpleClass assembly="XstreamTest, Version=1.0.2342.35727, Culture=neutral, PublicKeyToken=null"><a>7</a><x>testString</x></SimpleClass>
```
Now if you add this line:

```c#
    xstream.Alias("SimpleClass", typeof(SimpleClass));
```
you get:

```xml
    <SimpleClass?><a>7</a><x>testString</x></SimpleClass?>
```
you are now free to read this class back in like this. (Note: this should work but it doesn't):

```c#
    string classString = "<SimpleClass><a>1</a><b>2</b><x>testString</x></SimpleClass?>"; 
    XStream xstream = new XStream(); 
    xstream.Alias("SimpleClass", typeof(SimpleClass)); 
    SimpleClass simpRead = xstream.FromXml(classString) as SimpleClass; 
```
