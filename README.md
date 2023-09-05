# KDG-Technical-Exercise

## Hello Jim(s) and anyone else involved in this process!
My solutions to the technical exercise can be found in the Program.cs file. I will use this space to outline my workflow/process and also include my answer to question 9. To preface all answers, I was having some IDE issues and importing the classes was not working, so I ultimately ended up working with JArrays and JObjects. My apologies for this, as I am aware typed classes would have made my responses much more readable; I did however use explicit documentation to emphasize which entities were being used within each test question.

### Data Import
With filepaths already specified (thank you!) I used Newtonsoft JSON parsing to create four JArrays, one for each dataset.

### Q1
I figured best practice would be to include null checking within the data import, so all that was left to do here was check counts

### Q2
Considered abstracting counts from Q1 and Q2, but ultimately decided this was enough.

### Q3
Reused count variables from Q2.

### Q4
I noticed a few querying problems down the line that would use lambdas, so I used this one to brush off the rust syntactically.

### Q5
Ran into some issues with string comparison, which was easily fixed by using the more accurate string.Equals(). The Instructions PDF contained specific fields within the answer key, so I formatted my final answer accordingly.

### Q6
After finding the Thiel and Sons organization within the Organizations JSON file, I operated on the assumption that my query should NOT be case-sensitive.

### Q7
Switching to string.Equals() was also necessary for this case.

### Q8
I was able to abstract out a short method to search a given array for a given string within each EntityId field. This realization made me double back and search through prior problems for potential abstraction/refactoring, but ultimately I stuck with just this one. Side-note, the solution provided in the Instruction PDF does not include the total count.



