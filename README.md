Project Proposal CS 597 Enterprise Web Dev

Project Title:
Reimaging of Rockwell Automations VantagePoint Trend software

What is it going to do:
The project is going to be a reimaging of Rockwell Automations VantagePoint Trend software  which is a current technology utilized in the manufacturing sector to track data inside of automation facilities.  I will be focusing primarily on the graphing portion of VantagePoint as it is the only portion that I would have the capability of finishing this semester.  My project will provide real time graphs from a set of databases, currently only SQL server, which will be populated from data collected from PLC’s.  Because I will not have PLC’s available to me I will write a program to simulate the data that is being collected from them and push it into the database.  
The software itself will be a graphing utility in which a user can select a set of data points and plot them on a graph to look at both historical and real time updates of given data points.  Users will login and based on their level of clearance and affiliation be given a different set of data to interact with.  Data will be provided via a SQL server for historical data and use SignalR for real time communication.

Who is the target audience:
The audience of this application would be users who need to see the correlation between different sets of data in a graphical fashion.

What sort of data will it manage:
•	Mock Sensor data from a mock PLC
•	User information 
•	Data points

What web technologies will it use:
•	.NET MVC
•	.NET Web API
•	Twitter Bootstrap
•	SignalR
•	SQL Server
•	Charts.js


Stretch Goals:
•	OAuth authentication 
•	Saving of users charts
•	Look nice (I’m not a UI person so this is the toughest goal)

Links:
http://www.rockwellautomation.com/rockwellsoftware/products/factorytalk-vantagepoint.page#features


