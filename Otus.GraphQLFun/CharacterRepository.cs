using System.Collections.Generic;

namespace Otus.GraphQLFun
{
  class CharacterRepository : ICharacterRepository
  {
    public IEnumerable<Character> GetCharacters()
    {
      yield return new Character
      {
        Name = "R2-D2"
      };
      
      yield return new Character
      {
        Name = "Luke"
      };

      yield return new Character
      {
        Name = "Jon"
      };
    }
  }
}