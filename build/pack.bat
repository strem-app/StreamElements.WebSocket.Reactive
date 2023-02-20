set version=1.0.00
dotnet pack ../src/StreamElements.WebSocket -c Release -o ../../_dist /p:version=%version%
dotnet pack ../src/StreamElements.WebSocket.Reactive -c Release -o ../../_dist /p:version=%version%