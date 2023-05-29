Project simulating traffic management in a city,
It allows data to be stored in different representation  and provides a user interface for easy editing of the current data.
User is being given 10 commands: 
| name | desripton     | command syntax    |
|----- |---------------|---------------|
|list  |  lists chosen collection| list \<collection\> |
|add   |  adds new element to collection in chosen representation | add \<collection\> \<representation\> |
|edit  |  edits chosen item | edit \<collection\> \<predicate\> (\<fieldname\> (=\|\<\|\>) \<value\>)|
|find  |   prints items that satisfy given predicats | find \<collection\> \<predicate\> (same as edit) |
| delete| deletes items that satisfy giver predicats | delete \<collection\> \<predicate\> (same as edit) |
|undo  |   undoes last command            | undo |
|redo  |   redoes last undo            | redo |
|history  |     prints history of executed commands| history |
|export|   exports executed commands to xml or txt | export \<filename\> \<format\> (format = xml \| plaintext) |
|load  |   loads commands to be executed from given file | load \<filename\> |

Possible Data

Line
			- numberHex (string)
			- numberDec (int)
			- commonName (string)
			- stops (list of stops refs)
			- vehicles (list of vehicles refs)
			
Stop
			- id (int)
			- lines (list of lines refs)
			- name (string)
			- type (enum string)
			
Bytebus : Vehicle
			- id (int)
			- lines (list of lines refs)
			- engineClass (enum string)
		
Tram : Vehicle
			- id (int)
			- carsNumber (int)
			- line (line ref)

Driver
			- vehicles (list of vehicles refs)
			- name (string)
			- surname (string)
			- seniority (int)
