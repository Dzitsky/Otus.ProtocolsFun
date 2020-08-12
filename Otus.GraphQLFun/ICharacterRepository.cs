using System.Collections.Generic;

namespace Otus.GraphQLFun
{
  public interface ICharacterRepository
  {
    IEnumerable<Character> GetCharacters();
  }
}