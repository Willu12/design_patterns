Project simulating traffic management in a city,
It allows data to be stored in different representation  and provides a user interface for easy editing of the current data.
User is being given 10 commands: 
| name | desripton     | command syntax    |
|----- |---------------|---------------|
|list  |  lists chosen collection| list <collection> |
|add   |  adds new element to collection in chosen representation | add \<collection\> \<representation\> |
|edit  |  edits chosen item | edit <collection> <predicate> (<fieldname> (=|<|>) <value>)|
|find  |   prints items that satisfy given predicats | find <collection> <predicate> (same as edit) |
| delete| deletes items that satisfy giver predicats | delete <collection> <predicate> (same as edit) |
|undo  |   undoes last command            | undo |
|redo  |   redoes last undo            | redo |
|history  |     prints history of executed commands| history |
|export|   exports executed commands to xml or txt | export <filename> <format> (format = xml|plaintext) |
|load  |   loads commands to be executed from given file | load <filename> |
