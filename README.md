Based on an ERD, I have to do these:

Tasks should be completed using EntityFramework Core following the Code First
approach.
1. Prepare an endpoint that returns a list of all publishing houses. Publishing houses can be
filtered by city and/or country. If no filters are provided, unfiltered data should be returned.
The response should include all data related to the publishing house, the books it published
along with genres and authors. Data is sorted by publishing house country and name in
ascending order.
2. Prepare an endpoint that supports adding a new book. The endpoint should accept the
publishing house ID, authors' IDs as well as genres’ IDs and names. If genre does not exist,
we should support adding it.
