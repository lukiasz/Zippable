# Zippable
Tiny library with generic Zip implementation for C# enumerables and some small tools that make complex POCOs generation easier.

## Example

```
var names = new List<string> { "Adam", "Eve", "Michelle", "John" };
var surnames = new List<string> { "First", "Second", "Third", "Fourth" };

var results =
  from name in names.ToZippable()
  from surname in surnames.ToZippable()
  select name + " " + surname;
  
var expectedResults = new List<string>
{
  "Adam First",
  "Eve Second",
  "Michelle Third",
  "John Fourth"
};

Assert.Equal(expectedResults.Count(), results.Count());
Assert.All(results, result => expectedResults.Contains(result));
```

## Motivation

Apart from just being handy in [some cases](http://stackoverflow.com/questions/10297124/how-to-combine-more-than-two-generic-lists-in-c-sharp-zip), following this approach helped me a lot with generation of complex business objects basing on both real and fake data sources.

#### Well... why not just only one of .NET implementations of Faker?

Because they have generators of objects of type `Func<T>` (eg. `Faker.People.Name()`). Your other collections (db entities, static lists, external endpoints, whatever...) will surely be `IEnumerable<T>`'s. In more complex cases you'll probably end up with Faker plus custom lists of data and a lot of imperative glue per every class describing objects of your desire. This library may help you with reducing amount of imperative glue making your code cleaner and easier to maintain.

#### I don't use Faker, is it still useful?

If you're dealing with business objects generation - probably yes!

Comparing to calls to some methods (like `random.Next()` or `Faker.People.Name()`) the benefits are:

 * Code that generate objects doesn't need to know much about dependencies. When you need `IEnumerable` of some objects (to generate another `IEnumerable` of different objects), you don't need to think from where do they come from. This makes writing generators easier, especially in cases when data needs to follow some distribution.

 * `IEnumerable` is more convenient for enumerables in C# world because you can use `LINQ` and `foreach` loops :)

Anyway, you still need Faker or any other source of data because this library doesn't provide it. If your generator is of type `Func<T>`, use included `.Lift()` extension method to convert it to `IEnumerable<T>` or `.LiftToZippable()` if you want to additionally convert it to `Zippable`. In the same way you can wrap around .NET's `Random` object.

## Thanks

Jon Skeet for excelent [Reimplementing LINQ To Objects articles](https://codeblog.jonskeet.uk/2010/09/03/reimplementing-linq-to-objects-part-1-introduction/).
