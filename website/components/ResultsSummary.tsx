export default function ResultsSummary(props) {
  const { results } = props;

  return (
    <div>
      <span className="text-green-500">{results.validCount}</span> records uploaded successfully, <span className="text-red-500">{results.invalidCount}</span> records could not be uploaded.
    </div>
  );
}
