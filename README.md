# MenuParser
An XML parser for menu items, which also tags items pointing to a specified URL--and all their ancestor nodes--as "Active".  
<br/>
The XML parsing logic is mostly basic, but is distinguished by maintaining a stack to track which ancestor nodes lie above in the heirarchy, and imparts that to each menu-item node created.
<br/><br/>
This results in a retrograde-linked, B-tree-like structure--in which nodes are unaware of their children, but each node is aware of its complete parent chain.  While this does not support downward traversal, it can be traversed upward very efficiently to tag active parent nodes.  References to child nodes could, however, be added to the parsing routine to produce a doubly-linked tree. 
<br/><br/>
TO USE:
<br>
menuparser.exe "\<path to xml file describing the menu>" "\<string matching the active menu item's target URL>"
