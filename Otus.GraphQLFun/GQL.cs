using System.Collections.Generic;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace Otus.GraphQLFun
{
  public class Query
  {
    public string Hello() => "world";

    /// <summary>
    /// Gets all character.
    /// </summary>
    /// <param name="repository"></param>
    /// <returns>The character.</returns>
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public IEnumerable<Character> GetCharacters([Service] ICharacterRepository repository)
    {
      return repository.GetCharacters();
    }
  }

  public class CharacterType : ObjectType<Character>
  {
    protected override void Configure(IObjectTypeDescriptor<Character> descriptor)
    {
      descriptor
        .Field("Test")
        .Type<NonNullType<StringType>>()
        .Resolver(x => "MyTest");
    }
  }

  [ExtendObjectType(Name = nameof(Character))]
  public class CharacterResolvers
  {
    public int Age([Parent] Character indicator, [Service] ICharacterRepository repository)
    {
      return 1;
    }
  }
}