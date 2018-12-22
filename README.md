# Booking API

A RESTful API made in the .NET framework that allows clients to book seats remotely from a 100 seat space.

# Features

 - Allows users to book up to 4 seats per transaction
 - Allows user to book any specified seat, not at random
 - Easily compatible with front-end applications
 
  # Pre-requisites
  
  - .NET SDK Core 2.2 installed or later
  
  # JSON structure
  
The API reads in JSON and returns results in a JSON format. The API is capable of reading in a single JSON entry or an array of JSON entries up to 4. JSON entries must have all fields filled to work. An example of this is:

```json
{
  "seatNumber": "A2",
  "emailAddress": "bob@zupa.org.uk",
  "uniqueName": "bob",
  "booked": true
}
```
or

```json
{
    "seats": [
        {
            "seatNumber": "A2",
            "emailAddress": "bob@zupa.org.uk",
            "uniqueName": "bob",
            "booked": true
        },
        {
            "seatNumber": "A6",
            "emailAddress": "dave@zupa.org.uk",
            "uniqueName": "dave",
            "booked": true
        }
    ]
}
```
# Basic Usage
  
Call the API from localhost with the URL ```https://localhost:<port>/api/seat``` and then use one of the following HTTP commands:
  
# GET Request
   
Calling GET ```/api/seat``` will retrieve all of the 100 seats in the database, calling GET ```/api/seat/<seat-number>``` will retrieve the JSON data for that seat number.

# POST Request
   
Calling POST ```/api/seat``` with the correct JSON format will add a new seat to the database (for the server side if seat capacity increases).

# PUT Request
   
Calling PUT ```/api/seat``` with the correct JSON format will update an existing seat(s) in the database, calling PUT ```/api/seat/<seat-number>``` with a single JSON entry will update that given seat number.
 
 # DELETE Request
   
Calling DELETE ```/api/seat/<seat-number>``` will delete the specified seat number if it currently exists within the database.
