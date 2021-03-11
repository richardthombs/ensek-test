import Result from "./Result";

export default function Results(props) {
  const { results } = props;

  return (
    <div className="mt-16 bg-white bg-opacity-5 p-8">
      <h2 className="text-xl">Upload details</h2>
      <table>
        <thead>
          <tr>
            <th>Line</th>
            <th className="text-left px-4 py-2">Content</th>
            <th className="text-left px-4 py-2">Uploaded?</th>
          </tr>
        </thead>
        <tbody>
          {results.results.map(result => <Result key={result.lineNumber} result={result} />)}
        </tbody>
      </table>
    </div>
  );
}
