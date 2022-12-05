# Projektuppgift Systemintegration

## Uppgiftsbeskrivning:

- Skapa ett system som består av Microservices
- Systemet ska innehålla minst 2st APIer skrivna i C# som kommunicerar med varandra
- . . .

## **TODO:**

- [ ] find better solution for the https certificate stuff in deploy.yml
- [ ] implement JWT auth

## Commands

- `cd UrlShortenerAPI`
- - `docker build . -t urlshortenerapi`
- - `cd ../`
- `cd UrlRedirectService`
- - `docker build . -t urlredirectservice`
- - `cd ../`
- `docker-compose -f deploy.yml up`
