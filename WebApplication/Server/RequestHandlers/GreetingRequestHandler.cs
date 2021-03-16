// public void HandleGreeting()
//         {
//             DateTimeManager dateTimeManager = new DateTimeManager();
//             var timeText = $"the time on the server is {dateTimeManager.GetCurrentTime()} on {dateTimeManager.GetCurrentDate()}";

//             var peopleListString = "";
//             foreach (var person in _repository.GetPeopleList())
//             {
//                 peopleListString += person.Name + ' ';
//             }

//             var buffer = System.Text.Encoding.UTF8.GetBytes($"Hello {peopleListString}- {timeText}");
//             _context.Response.ContentLength64 = buffer.Length;
//             _context.Response.OutputStream.Write(buffer, 0, buffer.Length);
//             _context.Response.StatusCode = (int)HttpStatusCode.OK;
//         }