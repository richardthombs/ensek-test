export default function Result(props) {
  const { result } = props;

  return (
    <tr className={result.uploaded ? "text-green-500" : "text-red-500"}>
      <td className="text-right px-4 py-2">{result.lineNumber}</td>
      <td className="text-left px-4 py-2 font-mono">{result.content}</td>
      <td className="text-left px-4 py-2">{result.uploaded ? "Yes" : "No"}</td>
    </tr>
  );
}
