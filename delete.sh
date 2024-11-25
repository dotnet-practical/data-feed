for run_id in $(gh run list --status failure --limit 50 --json databaseId --jq '.[].databaseId'); do
  gh run delete $run_id
done
