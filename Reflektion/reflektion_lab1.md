==Reflektion labb 1==
1. Vad tror Du vi har för skäl till att spara det skrapade datat i JSON-format?
   Det är mer lättarbetat än XML och fungerar med alla möjliga typer av verktyg.

2. Olika jämförelsesiter är flitiga användare av webbskrapor. Kan du komma på fler typer av tillämplingar där webbskrapor förekommer?
   Alla sajter som vill sammanfatta information kan ha glädje av att skrapa webbsidor om informationen inte finns mer lättillgängliga på annat sätt. Det sparar tid för användaren som inte behöver kolla upp alla källor själv.
   Viss information kan analyseras och presentera statistik, som bl a kan visa trender. Det kan ex.vis användas i forskningssyfte eller i vinstintresse.
   
3. Hur har du i din skrapning underlättat för serverägaren?
   Jag har lagt till kontaktuppgifter och förklarat syftet med skrapningen i useragent.
   Är det ett script som skulle köras kontinuerligt kulle jag överväga att sprida ut anropen så att servern inte belastas för mycket och orsakar att sajten upplevs som seg för manuella besökare.

4. Vilka etiska aspekter bör man fundera kring vid webbskrapning?
   Hur man använder informationen, det kanske inte är helt ok att skrapa information för att konkurrera med den man skrapar information från. Kan dessutom vara rättsvidrigt.
   Den man skrapar information från betalar för resurser som bandbredd och trafikmängd. Det är därför viktigt att man försöker att inte belastar servern mer än nödvändigt. Skriver kod som är vänlig mot servern och som inte gör onödiga anrop.

5. Vad finns det för risker med applikationer som innefattar automatisk skrapning av webbsidor? Nämn minst ett par stycken!
   Applikationen kan "löpa amok" och göra oväntade saker. Den kan hamna i en loop med ständiga serverförfrågningar, vilket kan påverka servern avsevärt, även min egen.

6. Tänk dig att du skulle skrapa en sida gjord i ASP.NET WebForms. Vad för extra problem skulle man kunna få då?
   Problem på grund av förfrågningsvalidering. Man måste bibehålla och skicka med ViewState i Header(..?)

7. Välj ut två punkter kring din kod du tycker är värd att diskutera vid redovisningen. Det kan röra val du gjort, tekniska lösningar eller lösningar du inte är riktigt nöjd med.
   Om du har kunskap om Asp.net Web api: Jag valde att skapa min webbskrapa i ASP.NET Web API som en utmaning för mig själv att testa på något nytt. Jag behövde dock skapa en ny sajt för att kunna köra den. Det känns lite knäppt. Tänk om man vill ha 100 script som gör olika saker? Behöver man 100 sajter då? Med PHP hade alla scripten kunnat ligga i samma mapp till och med. Tips på hur man ska tänka?
   Som syns i koden så blir det väldigt många kontroller av att den aktuella noden inte är null. Jag undrar om du har något tips på alternativ för platta ihop koden lite utan att riskera att få ett undantagsfel på grund av att jag använder ett null-objekt.

8. Hitta ett rättsfall som handlar om webbskrapning. Redogör kort för detta.
   RyanAir vill sälja sina biljetter själva på sin sajt RyanAir.com. Webbsajten Atrapalo.com skrapade information från RyanAirs webbsajt och sålde biljetterna på sin egen sajt med kostnadstillägg. En spansk domstol gav år 2010 RyanAir rätt mot Atrapalo. RyanAir meddelade dock att sajter som är genuint intresserade av att tillhandahålla en tjänst med information om flygbolagets priser, utan extra avgifter för kunden, är välkomna till samarbete med RyanAir, så även Atrapalo.

9. Känner du att du lärt dig något av denna uppgift?
   Ja Asp.Net Web API var nytt men kändes trevligt att det var liknande Asp.Net MVC. Men som med allt nytt har det tagit alldeles för mycket tid i förhållande till vad uppgiften gick ut på, så jag har inte hunnit med extrauppgift 2 och 3. Tänkte nog att valet av Web api fick fungera lite som "extrauppgift".
   XPath har jag aldrig använt tidigare och det var roligt att prova. Tyckte det fungerade riktigt bra.
   Jag trodde att skrapningen i sig skulle vara något mindre tidskrävande. Nyttigt att få kolla lite på html "från andra hållet" och se hur vissa designval kan påverka enkelheten att arbeta med koden en hel del.
